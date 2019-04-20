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

            _wallPoster = new WallPoster();
            _wallReader = new WallReader(dateDiff);
        }

        public void Post(string userName, string text)
        {
            _wallPoster.Post(userName, text);
        }

        public IList<HistoricPost> Read(string userName)
        {
            return _wallReader.Read(userName);
        }
    }
}

