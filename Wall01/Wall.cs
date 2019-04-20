using System;
using System.Collections.Generic;
using System.Xml.Xsl;
using Wall01;

namespace Wall01
{
    public class Wall
    {
        private readonly WallPoster _wallPoster;
        private readonly WallReader _wallReader;

        public Wall(IDateDiff dateDiff = null)
        {
            if (dateDiff == null)
            {
                dateDiff = new DateDiff();
            }
            var usersRepository = new UsersRepository();
            _wallPoster = new WallPoster(usersRepository);
            _wallReader = new WallReader(dateDiff, usersRepository);
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
            return _wallReader.Read(userName);
        }
    }
}

