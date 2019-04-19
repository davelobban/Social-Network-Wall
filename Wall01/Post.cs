using System;

namespace Wall01
{
    public class Post
    {
        public string User { get; }
        public string Text { get; }
        public DateTime Timestamp { get; }
        public string TimeSince { get; private set; }

        public Post(string user, string text, DateTime timestamp)
        {
            User = user;
            Text = text;
            Timestamp = timestamp;
        }

        public void CalcTimeSince(IDateDiff dateDiff)
        {
            TimeSince = dateDiff.GetTimeSincePosted(this);
        }
    }
}