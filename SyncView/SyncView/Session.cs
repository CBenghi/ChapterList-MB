using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SyncView
{

    class Session : IEquatable<Session>
    {
        private string l;
        private string p;
        
        public Session(string l, string p)
        {
            this.l = l;
            this.p = p;
        }

        public string GetMediaTitle(ChapterListMB.SyncView.SyncViewRepository repo)
        {
            var f = GetAudioFile(repo);
            if (f == null)
                return "";
            var file = TagLib.File.Create(f.FullName);
            return file.Tag.Title;
        }

        public override string ToString()
        {
            return $"L{l}P{p}";
        }

        /// <param name="transcriptFileName">Bare transcript file name, no path.</param>
        internal static Session FromTranscriptFile(string transcriptFileName)
        {
            var m = regexGetLectureAndPart.Match(transcriptFileName);
            if (!m.Success)
            {
                return null;
            }
            return new Session(m.Groups["L"].Value, m.Groups["P"].Value);
        }

        private static Regex reImageNameParts = new Regex(@"\\L(?<L>\d+)\\P(?<P>\d+)\\", RegexOptions.Compiled);

        public static Session FromImageFile(FileInfo fileInfo)
        {
            var m = reImageNameParts.Match(fileInfo.FullName);
            if (m.Success)
            {
                var L = m.Groups["L"].Value;
                var P = m.Groups["P"].Value;
                return new Session(L, P);
            }
            return null;
        }

       static Regex regexGetLectureAndPart { get; } = new Regex(@"L(?<L>\d+)P(?<P>\d+)", RegexOptions.Compiled);

        /// <summary>
        /// Lecture code
        /// </summary>
        public string L
        {
            get => l;
            set
            {
                l = value;
                while (l.Length < 2)
                    l = "0" + l;
            }
        }

        /// <summary>
        /// Part code
        /// </summary>
        public string P
        {
            get => p;
            set
            {
                p = value;
                while (p.Length < 2)
                    p = "0" + p;
            }
        }

        internal FileInfo GetAudioFile(ChapterListMB.SyncView.SyncViewRepository repo)
        {
            var path = Path.Combine(repo.AudioFolder.FullName, $"L{L}");

            DirectoryInfo d = new DirectoryInfo(path);
            return d.GetFiles($"L{L}P{P}*.mp3").FirstOrDefault();
        }

        public bool Equals(Session other)
        {
            if (other == null)
                return false;
            return p == other.P && l == other.L;
        }
    }

}
