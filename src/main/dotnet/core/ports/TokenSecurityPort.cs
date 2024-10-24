using Core.Domain.Models;

namespace Core.Ports
{
    public interface TokenSecurityPort
    {
        string GenerateToken(User user);
        User ValidateToken(string token);
        bool IsTokenValid(string token);
    }
}