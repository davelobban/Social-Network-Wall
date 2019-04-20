using System;
using System.Collections.Generic;
using System.Linq;

namespace Wall01
{
    public interface IPostIdProvider
    {
        int GetNextId();
    }

    public class UsersRepository : IPostIdProvider
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

        public void Post(string userName, string text)
        {
            var timestamp = DateTime.Now;
            var user = GetUser(userName);
            user.AddPost(userName, text, timestamp, this);
        }

        public int GetNextId()
        {
            return _postingUsers.Sum(u=>u.Posts.Count);
        }
    }
}