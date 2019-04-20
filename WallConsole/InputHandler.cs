using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wall01;

namespace WallConsole
{
    public class InputHandler
    {
        private IDateDiff _dateDiff;
        private Wall01.Wall _wall;
        public InputHandler(IDateDiff dateDiff)
        {
            _dateDiff = dateDiff;
            _wall= new Wall(_dateDiff);
        }

        public List<string> AcceptInput(string input)
        {
            var postDelimiter = "->";
            var delimiterChunks = input.Split(postDelimiter);
            if (delimiterChunks.Length == 2)
            {
                _wall.Post(delimiterChunks[0].Trim(), delimiterChunks[1].Trim());
                return new List<string>();
            }

            if (input.IndexOf(" ") < 0)
            {
                var responses=_wall.Read(input);
                //$"{post.Text} {TimeSince}"
                return responses.Select(r => $"{r.Text} {r.TimeSince}").ToList();
            }
            return new List<string>();
        }
    }
}
