using System;
using System.Collections.Generic;
using System.Linq;

namespace Wall01
{
    public class WallPoster
    {
        private UsersRepository _usersRepository;

        public WallPoster(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public void Post(string userName, string text)
        {
            var timestamp = DateTime.Now;
            var user = GetUser(userName);
            user.AddPost(userName, text, timestamp);
        }
        private User GetUser(string userName)
        {
            return _usersRepository.GetUser(userName);
        }
    }
}