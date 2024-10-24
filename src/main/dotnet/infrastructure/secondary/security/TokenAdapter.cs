using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Core.Domain.Models;
using Core.Ports;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Secondary.Security
{
    public class TokenAdapter : TokenSecurityPort
    {
        private readonly string _secretKey;

        public TokenAdapter(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("username", user.Username),
                new Claim("roles", JsonSerializer.Serialize(user.Roles ?? new List<string>()))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = principal.FindFirst(ClaimTypes.Name)?.Value;
                var roles = principal.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

                return new User
                {
                    Id = userId,
                    Username = username,
                    Roles = roles
                };
            }
            catch (Exception)
            {
                throw new SecurityTokenException("Token inválido.");
            }
        }

        public bool IsTokenValid(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return validatedToken != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}