namespace Wall01
{
    public class HistoricPost : Post
    {
        public string TimeSince { get; private set; }
        public string FormattedOutputPrependedWithUserName { get; private set; }

        public HistoricPost(Post post, IDateDiff dateDiff) : base(post.User, post.Text, post.Timestamp)
        {
            CalcTimeSince(dateDiff);
            FormattedOutputPrependedWithUserName = $"{post.User} - {post.Text} {TimeSince}";
        }

        public void CalcTimeSince(IDateDiff dateDiff)
        {
            TimeSince = dateDiff.GetTimeSincePosted(this);
        }
    }
}