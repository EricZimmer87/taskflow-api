using TaskFlow.API.Models;

namespace TaskFlow.API.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
