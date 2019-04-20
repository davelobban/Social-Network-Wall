using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wall01;

namespace WallConsole
{
    public class InputHandler
    {
        private Wall01.Wall _wall;
        public InputHandler(IDateDiff dateDiff)
        {
            _wall= new Wall(dateDiff);
        }

        public List<string> AcceptInput(string input)
        {
            if (Post(input, out var list)) return list;

            if (Read(input, out var acceptInput1)) return acceptInput1;

            if (Follow(input, out var list1)) return list1;

            if (Wall(input, out var acceptInput2)) return acceptInput2;


            return new List<string>();
        }

        private bool Wall(string input, out List<string> list)
        {
            list = new List<string>();
            var wallDelimiter = " wall";
            if (input.IndexOf(wallDelimiter) > 0)
            {
                var responses = _wall.GetWall(input.Replace(wallDelimiter, string.Empty));
                {
                    list = responses.Select(r => r.FormattedOutputPrependedWithUserName).ToList();
                    return true;
                }
            }

            return false;
        }

        private bool Follow(string input, out List<string> list)
        {
            list = new List<string>();
            var followsDelimiter = "follows";
            var followsChunks = input.Split(followsDelimiter);
            if (followsChunks.Length == 2)
            {
                _wall.Follow(followsChunks[0].Trim(), followsChunks[1].Trim());
                {
                    return true;
                }
            }

            return false;
        }

        private bool Read(string input, out List<string> list)
        {
            list = new List<string>();
            if (input.IndexOf(" ") < 0)
            {
                var responses = _wall.Read(input);
                {
                    list = responses.Select(r => $"{r.Text} {r.TimeSince}").ToList();
                    return true;
                }
            }

            return false;
        }

        private bool Post(string input, out List<string> list)
        {
            list = new List<string>();
            var postDelimiter = "->";
            var delimiterChunks = input.Split(postDelimiter);
            if (delimiterChunks.Length == 2)
            {
                _wall.Post(delimiterChunks[0].Trim(), delimiterChunks[1].Trim());
                {
                    return true;
                }
            }

            return false;
        }
    }
}
