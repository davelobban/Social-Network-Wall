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


        public IList<Post> Read(string userName)
        {
            var user = GetUser(userName);
            var posts = user.Posts;
            foreach (var post in posts)
            {
                post.CalcTimeSince(_dateDiff);
            }
            return posts;
        }
    }

    public class User
    {
        public string Name { get; }
        public IList<Post> Posts { get; } = new List<Post>();

        public User(string name)
        {
            Name = name;
        }

        public void AddPost(string userName, string text, DateTime timestamp)
        {
            Posts.Add(new Post(Name,text,timestamp));
        }
    }
}

