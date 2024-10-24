using Core.Domain.Models;
using Core.Ports;

namespace Infrastructure.Persistence
{
    public class UserPersistenceAdapter : UserPersistencePort
    {
        private readonly List<User> _users;

        public UserPersistenceAdapter()
        {
            _users = new List<User>();
        }

        public User GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }
    }
}
