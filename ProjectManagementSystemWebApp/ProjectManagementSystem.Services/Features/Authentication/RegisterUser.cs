using System.Threading;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Entities;
using ProjectManagementSystem.Services.DTOs.UserDTOs;
using StackExchange.Redis;


namespace ProjectManagementSystem.Services.Features.Authentication
{
    public class RegisterUser
    {
        public class RegisterUserCommand : IRequest<UserDto> 
        { 
            public UserDto User { get; set; }

            public RegisterUserCommand(UserDto user) 
            {
                User = user;
            }
        }
        public class Handler : IRequestHandler<RegisterUserCommand, UserDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _logger = logger;
                _unitOfWork = unitOfWork;
            }
            public async Task<UserDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    if(string.IsNullOrEmpty(command.User.Email) || string.IsNullOrEmpty(command.User.Password))
                    {
                        throw new Exception("Please fill the credentials");
                    }
                    if(await IsUserExist(command,cancellationToken))
                    {
                        _logger.LogError("User Already Exists, Try Login");
                        throw new Exception("User already Exist");
                    }
                    var hashPassword = BCrypt.Net.BCrypt.HashPassword(command.User.Password);
                    var user = new User
                    {
                        Email = command.User.Email,
                        Password = hashPassword,
                        UserName = command.User.UserName,
                        Role = command.User.Role
                    };

                    await _unitOfWork.UserRepository.AddUserAsync(user, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return new UserDto { Email = user.Email };
                }
                catch(Exception exception)
                {
                    _logger.LogError(exception, "There is a error");
                    throw new Exception("There is a error in registration.");
                }
                                              
            }

            private async Task<bool> IsUserExist(RegisterUserCommand command, CancellationToken cancellationToken)
            {
                var existingUser = (await _unitOfWork.UserRepository.GetAllUsersAsync(cancellationToken))
                                    .FirstOrDefault(user => user.Email == command.User.Email);
                if (existingUser != null )
                {
                    return true;
                }
                return false;
            }
        }

    }
}
