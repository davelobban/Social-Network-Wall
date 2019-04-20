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
            _usersRepository.Post(userName, text);
        }
       
    }
}