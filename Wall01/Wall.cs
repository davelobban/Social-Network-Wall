using System;

namespace Wall01
{
    public class Wall
    {

        private Post _post;
        public void Post(string user, string text)
        {
            var timestamp = DateTime.Now;
            _post = new Post(user, text, timestamp);
        }

        public Post Read(string alice)
        {
            return _post;
        }
    }
}
