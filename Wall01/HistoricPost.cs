using System;

namespace Wall01
{
    public class HistoricPost
    {
        private Post _post;
        public string TimeSince { get; private set; }
        public string FormattedOutputPrependedWithUserName { get; }
        public string User => _post.User;
        public string Text => _post.Text;
        public DateTime Timestamp => _post.Timestamp;
        public int Id => _post.Id;
        public HistoricPost(Post post, IDateDiff dateDiff)
        {
            _post = post;
            CalcTimeSince(dateDiff);
            FormattedOutputPrependedWithUserName = $"{post.User} - {post.Text} {TimeSince}";
        }

        public void CalcTimeSince(IDateDiff dateDiff)
        {
            TimeSince = dateDiff.GetTimeSincePosted(_post);
        }
    }
}