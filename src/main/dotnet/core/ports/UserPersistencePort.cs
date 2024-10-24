using Core.Domain.Models;

namespace Core.Ports
{
    public interface UserPersistencePort
    {
        User GetUserByUsername(string username);
        void AddUser(User user);
    }
}
