using System;
using System.Collections.Generic;
using System.Text;

namespace Wall01
{
    public interface IDateDiff
    {
        string GetTimeSincePosted(Post post);
    }
}
