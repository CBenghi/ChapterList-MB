using Accord.Math.Geometry;
using ChapterListMB.SyncView;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SyncView
{
    class Bookmark : IComparable<Bookmark>
    {
        private static Regex reImageNameParts = new Regex(@"\\L(?<L>\d+)\\P(?<P>\d+)\\(?<t>\d+) *- *(?<txt>.*)", RegexOptions.Compiled);

        internal Session session { get; set; }

        internal int Timing { get; set; }

        internal enum SourceType
        {
            Image = 1,
            Transcript = 2,
            Skip = 3
        }

        public static Bookmark FromImageFile(FileInfo fileInfo)
        {
            var m = reImageNameParts.Match(fileInfo.FullName);
            if (m.Success)
            {
                var ret = new Bookmark();

                var L = m.Groups["L"].Value;
                var P = m.Groups["P"].Value;

                ret.session = new Session(L, P);
                ret.Timing = ImageInfo.GetMillisecondsFromFileName(fileInfo);
   
                ret.Type = SourceType.Image;
                var nm = m.Groups["txt"].Value;
                if (nm == ".png")
                {
                    nm = fileInfo.Name;
                }
                ret.Text = nm;
                return ret;
            }
            return null;
        }

        internal SourceType Type { get; set; }

        internal string Text { get; set; }

        public int CompareTo(Bookmark other)
        {
            if (other == null)
                return 1;

            int t = 0;
            if (session != null && other.session != null)
            {
                t = session.ToString().CompareTo(other.session.ToString());
            }
            if (t != 0)
                return t;
            t = Timing.CompareTo(other.Timing);
            if (t != 0)
                return t;
            t = Type.CompareTo(other.Type);
            return t;
        }

        internal int GetSkipIndex()
        {
            var restIndex = Text.IndexOf("#skip");
            var rest = Text.Substring(restIndex+5).Trim();

            // Int => absolute value in milliseconds
            // +/-Int => Relative to current position

            if (Int32.TryParse(rest, out var parsed))
            {
                if (rest.StartsWith("-") || rest.StartsWith("+"))
                {
                    return Timing + parsed;
                }
                return parsed;
            }
            
            return -1;
        }
    }

}
