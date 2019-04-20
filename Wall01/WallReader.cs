﻿using System.Collections.Generic;
using System.Linq;

namespace Wall01
{
    public class WallReader
    {
        private readonly IDateDiff _dateDiff;
        private UsersRepository _usersRepository;

        public WallReader(IDateDiff dateDiff, UsersRepository usersRepository)
        {
            _dateDiff = dateDiff;
            _usersRepository = usersRepository;
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
            historicPosts.Reverse();
            return historicPosts;
        }
        private User GetUser(string userName)
        {
            return _usersRepository.GetUser(userName);
        }
    }
}