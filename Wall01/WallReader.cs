using System.Collections.Generic;

namespace Wall01
{
    public class WallReader
    {
        private readonly IDateDiff _dateDiff;

        public WallReader(IDateDiff dateDiff)
        {
            _dateDiff = dateDiff;
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
        private User GetUser(string userName)
        {
            return Users.GetUser(userName);
        }
    }
}