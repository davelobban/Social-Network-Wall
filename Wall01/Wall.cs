using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Xsl;
using Wall01;

namespace Wall01
{
    public class Wall
    {

        private IList<User> _users;
        private IDateDiff _dateDiff;

        public Wall(IDateDiff dateDiff = null)
        {
            if (dateDiff == null)
            {
                dateDiff = new DateDiff();
            }

            _dateDiff = dateDiff;

            _users = new List<User>();
        }

        public void Post(string userName, string text)
        {
            var timestamp = DateTime.Now;
            var user = GetUser(userName);
            if (user == null)
            {
                user = new User(userName);
                _users.Add(user);
            }

            user.AddPost(userName, text, timestamp);
        }

        private User GetUser(string userName)
        {
            var user = _users.FirstOrDefault(u => u.Name == userName);
            return user;
        }


        public IList<HistoricPost> Read(string userName)
        {
            var user = GetUser(userName);
            var posts = user.Posts;
            var historicPosts = new List<HistoricPost>();
            foreach (var post in posts)
            {
                historicPosts.Add(new HistoricPost(post, _dateDiff));
            }
            return historicPosts;
        }
    }
}

