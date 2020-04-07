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
            Image = 0,
            Transcript = 1
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
                ret.Text = m.Groups["txt"].Value;
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
            var t = session.ToString().CompareTo(other.session.ToString());
            if (t != 0)
                return t;
            t = Timing.CompareTo(other.Timing);
            if (t != 0)
                return t;
            t = Type.CompareTo(other.Type);
            return t;
        }
    }

}
