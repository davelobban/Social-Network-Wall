using System;
using System.Collections.Generic;
using System.Linq;

namespace Wall01
{
    public class WallPoster
    {
        
        public void Post(string userName, string text)
        {
            var timestamp = DateTime.Now;
            var user = GetUser(userName);
            user.AddPost(userName, text, timestamp);
        }
        private User GetUser(string userName)
        {
            return Users.GetUser(userName);
        }
    }
}