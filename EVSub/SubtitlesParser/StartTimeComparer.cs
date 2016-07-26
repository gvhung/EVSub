using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesParser
{
    class StartTimeComparer : IComparer<SubtitleItem>
    {
        public int Compare(SubtitleItem x, SubtitleItem y)
        {
            return x.StartTime - y.StartTime;
        }
    }
}
