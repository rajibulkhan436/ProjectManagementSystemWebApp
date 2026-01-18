using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Core.Contracts;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.DTOs.UserDTOs;


namespace ProjectManagementSystem.Services.Features.Authentication
{
    public class LoginUser
    {
        public class LoginUserQuery : IRequest<UserLoginDto>
        {
            public UserLoginDto Login { get; set; }

            public LoginUserQuery(UserLoginDto login)
            {
                Login = login;
            }
        }
        public class Handler : IRequestHandler<LoginUserQuery, UserLoginDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;
            private readonly IRedisService _redisService;
            private readonly ITokenService _tokenService;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger, IRedisService redisService, ITokenService tokenService)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
                _redisService = redisService;
                _tokenService = tokenService;
            }

            public async Task<UserLoginDto> Handle(LoginUserQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    if (string.IsNullOrEmpty(query.Login.Email) || string.IsNullOrEmpty(query.Login.Password))
                    {
                        throw new Exception("Kindly Fill The credentials");
                    }
                    var userList = await _unitOfWork.UserRepository.GetAllUsersAsync(cancellationToken);
                    var requiredUser = userList.FirstOrDefault(user => user.Email == query.Login.Email);
                    if (requiredUser == null) 
                    {
                        throw new Exception("User Not Found, Try Register");
                    }
                    if (!BCrypt.Net.BCrypt.Verify(query.Login.Password, requiredUser.Password))
                    {
                        throw new Exception("Invalid password.");
                    }

                    UserLoginDto loginUser = new UserLoginDto
                    {
                        Email = query.Login.Email,
                        Role = requiredUser.Role,
                        Id = requiredUser.Id,
                    };
                    var token = _tokenService.GenerateToken(loginUser);
                    var serializedUser = Newtonsoft.Json.JsonConvert.SerializeObject(requiredUser);
                    var tokenExpiry = TimeSpan.FromMinutes(30);

                    await _redisService.SetValueAsync(token, serializedUser, cancellationToken);

                    return new UserLoginDto 
                                  { 
                                    Email = query.Login.Email,
                                    Role = requiredUser.Role,
                                    UserName = requiredUser.UserName,
                                    Token = token, 
                                    Id = requiredUser.Id,
                                   };
                }
                catch (Exception exception) 
                {
                    _logger.LogError(exception, "Error caused in Login.");
                    throw new Exception("There is an error in login");
                }

            }

        }
    }
}
