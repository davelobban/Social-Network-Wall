using System;

namespace Wall01
{
    public class Post
    {
        public Post(string user, string text, DateTime timestamp, IPostIdProvider postIdProvider)//, int id)
        {
            User = user;
            Text = text;
            Timestamp = timestamp;
            Id = postIdProvider.GetNextId();
        }
        //public Post(string user, string text, DateTime timestamp)
        //{
        //    User = user;
        //    Text = text;
        //    Timestamp = timestamp;
        //}
        public string User { get; }
        public string Text { get; }
        public DateTime Timestamp { get; }
        public int Id{ get; }

    }
}