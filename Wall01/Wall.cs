using System;

namespace Wall01
{
    public class Wall
    {

        private Post _post;
        private IDateDiff _dateDiff;

        public Wall(IDateDiff dateDiff)
        {
            _dateDiff = dateDiff;
        }

        public void Post(string user, string text)
        {
            var timestamp = DateTime.Now;
            _post = new Post(user, text, timestamp);
        }

        public Post Read(string alice)
        {
            var post=_post;
            post.CalcTimeSince(_dateDiff);
            return post;
        }
    }
}
