using System;
using System.Collections.Generic;

namespace Wall01
{
    public class User
    {
        public string Name { get; }
        public IList<Post> Posts { get; } = new List<Post>();

        public User(string name)
        {
            Name = name;
        }

        public void AddPost(string userName, string text, DateTime timestamp, IPostIdProvider postIdProvider)//, int Id)
        {
            Posts.Add(new Post(Name,text,timestamp, postIdProvider));
        }
    }
}