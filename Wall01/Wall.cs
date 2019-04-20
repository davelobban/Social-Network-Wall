using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Xsl;
using Wall01;

namespace Wall01
{
    public class Wall
    {
        private readonly WallPoster _wallPoster;
        private readonly WallReader _wallReader;
        private IDictionary<User, List<User>> _following;
        private UsersRepository _usersRepository;

        public Wall(IDateDiff dateDiff = null)
        {
            if (dateDiff == null)
            {
                dateDiff = new DateDiff();
            }
            _usersRepository = new UsersRepository();
            _following = new Dictionary<User, List<User>>();
            _wallPoster = new WallPoster(_usersRepository);
            _wallReader = new WallReader(dateDiff, _usersRepository);
        }

        public void Post(string userName, string text)
        {
            _wallPoster.Post(userName, text);
        }

        public IList<HistoricPost> Read(string userName)
        {
            return _wallReader.Read(userName);
        }

        public IList<HistoricPost> GetWall(string userName)
        {
            var follower = _usersRepository.GetUser(userName);
            if (_following.ContainsKey(follower))
            {
                var following = _following[follower];

                return _wallReader.Read(following, follower);
            }
            return _wallReader.Read(userName);
        }

        public void Follow(string followerUserName, string target)
        {
            var follower = _usersRepository.GetUser(followerUserName);
            if (_following.ContainsKey(follower) == false)
            {
                _following.Add(follower, new List<User>());
            }

            var followerFollows = _following.Single(f => f.Key == follower);
            var targetUser = _usersRepository.GetUser(target);
            followerFollows.Value.Add(targetUser);
        }
    }
}

