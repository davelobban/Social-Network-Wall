using System.Collections.Generic;
using System.Linq;

namespace Wall01
{
    public class UsersRepository
    {
        private IList<User> _postingUsers = new List<User>();

        public User GetUser(string userName)
        {
            var user = _postingUsers.FirstOrDefault(u => u.Name == userName);
            if (user == null)
            {
                user = new User(userName);
                _postingUsers.Add(user);
            }
            return user;
        }
    }
}