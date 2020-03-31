using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ChapterListMB.SyncView
{

    class ImageInfo
    {
        Regex r = new Regex(@"^(\d+) -", RegexOptions.Compiled);

        internal FileInfo f { get; set; }
        internal int computedTimeStampMilliseconds { get; set; } = -1;

        internal ImageInfo(FileInfo file)
        {
            f = file;
            var m = r.Match(file.Name);
            if (m.Success)
            {
                int seconds;
                int minutes = 0;
                var integersString = m.Groups[1].Value;
                if (integersString.Length < 3)
                {
                    seconds = Convert.ToInt32(integersString);
                }
                else
                {
                    var secString = integersString.Substring(integersString.Length - 2);
                    var minString = integersString.Substring(0, integersString.Length - 2);
                    seconds = Convert.ToInt32(secString);
                    minutes = Convert.ToInt32(minString);
                }
                seconds = minutes * 60 + seconds;
                computedTimeStampMilliseconds = seconds * 1000;
            }
        }

    }
}
