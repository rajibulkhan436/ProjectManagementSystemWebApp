using ProjectManagementSystem.Services.DTOs.UserDTOs;

namespace ProjectManagementSystem.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(UserLoginDto user);

        bool VerifyToken(string token);
    }
}
