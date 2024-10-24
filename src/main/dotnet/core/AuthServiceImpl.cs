using Core.Domain.Models;
using Core.Ports;

namespace Core.UseCases.Impl
{
    public class AuthServiceImpl : AuthUseCase
    {
        private readonly UserPersistencePort _userRepository;
        private readonly TokenSecurityPort _tokenSecurityPort;

        public AuthServiceImpl(UserPersistencePort userRepository, TokenSecurityPort tokenSecurityPort)
        {
            _userRepository = userRepository;
            _tokenSecurityPort = tokenSecurityPort;
        }

        public Token SignIn(User user)
        {
            var existingUser = _userRepository.GetUserByUsername(user.Username);

            if (existingUser == null || !VerifyPassword(user.Password, existingUser.Password))
            {
                throw new UnauthorizedAccessException("Credenciales inválidas.");
            }

            var accessToken = _tokenSecurityPort.GenerateToken(existingUser);

            return new Token
            {
                AccessToken = accessToken
            };
        }

        public void SignUp(User user)
        {
            var existingUser = _userRepository.GetUserByUsername(user.Username);

            if (existingUser != null)
            {
                throw new InvalidOperationException("El usuario ya existe.");
            }

            user.Password = HashPassword(user.Password);
            user.Roles = new List<string> { "User" };

            _userRepository.AddUser(user);
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(enteredPassword)) == storedPassword;
        }
    }
}
