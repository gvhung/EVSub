using System;
using System.Collections.Generic;

namespace SubtitlesParser
{
    public class SubtitleItem
    {

        //Properties------------------------------------------------------------------

        //StartTime and EndTime times are in milliseconds
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public List<string> Lines { get; set; }


        //Constructors-----------------------------------------------------------------

        /// <summary>
        /// The empty constructor
        /// </summary>
        public SubtitleItem()
        {
            this.Lines = new List<string>();
        }


        // Methods --------------------------------------------------------------------------

        public override string ToString()
        {
            return removeFormatSrtSub(string.Join(Environment.NewLine, Lines));
        }
        public string getStartTimeSpan()
        {
            var startTs = new TimeSpan(0, 0, 0, 0, StartTime);
            return startTs.ToString("G");
        }
        public string getEndTimeSpan()
        {
            var endTs = new TimeSpan(0, 0, 0, 0, EndTime);
            return "->" + endTs.ToString("G");
        }
        public string getAll()
        {
            var startTs = new TimeSpan(0, 0, 0, 0, StartTime);
            var endTs = new TimeSpan(0, 0, 0, 0, EndTime);

            var res = string.Format("{0} --> {1}: {2}", startTs.ToString("G"), endTs.ToString("G"), string.Join(Environment.NewLine, Lines));
            return res;
        }
        private string removeFormatSrtSub(string SrtSub)
        {
            string tmp = SrtSub;
            tmp = tmp.Replace("<b>", "").Replace("<i>", "").Replace("<u>", "");
            tmp = tmp.Replace("</b>", "").Replace("</i>", "").Replace("</u>", "");
            return tmp;
        }
    }
}