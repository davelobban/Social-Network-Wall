using System;

namespace Wall01
{
    public class Wall
    {
        private string _text;
        public void Post(string alice, string text)
        {
            _text = text;
        }

        public string Read(string alice)
        {
            return _text;
        }
    }
}
