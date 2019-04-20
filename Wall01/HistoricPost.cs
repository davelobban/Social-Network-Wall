namespace Wall01
{
    public class HistoricPost : Post
    {
        public string TimeSince { get; private set; }

        public HistoricPost(Post post, IDateDiff dateDiff) : base(post.User, post.Text, post.Timestamp)
        {
            CalcTimeSince(dateDiff);
        }

        public void CalcTimeSince(IDateDiff dateDiff)
        {
            TimeSince = dateDiff.GetTimeSincePosted(this);
        }
    }
}