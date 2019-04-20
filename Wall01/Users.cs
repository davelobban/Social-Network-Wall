using System.Collections.Generic;
using System.Linq;

namespace Wall01
{
    public static class Users
    {
        private static IList<User> PostingUsers = new List<User>();

        public static User GetUser(string userName)
        {
            var user = PostingUsers.FirstOrDefault(u => u.Name == userName);
            if (user == null)
            {
                user = new User(userName);
                PostingUsers.Add(user);
            }
            return user;
        }
    }
}