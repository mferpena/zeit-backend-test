using Core.Domain.Models;

namespace Core.UseCases
{
    public interface AuthUseCase
    {
        Token SignIn(User user);
        void SignUp(User user);
    }
}
