using Microsoft.VisualBasic.FileIO;
using SyncView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChapterListMB.SyncView
{
    partial class SyncViewRepository
    {
        internal string mediaFileName { get; private set; }
        
        internal TimedObjects<ImageInfo> Images;
        
        internal TimedObjects<PointerCoordinates> Pointers;

        private static string transcriptsFolder = @"C:\Data\Work\Esame Stato\SupportingMedia\Transcripts\";

        internal string GetLectureCode()
        {
            var m = regexGetLectureAndPart.Match(mediaFileName);
            if (m.Success)
                return m.Value;
            return "";
        }

        public IEnumerable<string> GetTranscriptsText(string filter)
        {
            var fp = GetAssociatedLyricsFile();
            if (fp == null || !fp.Exists)
                return Enumerable.Empty<string>();
            return GetLyricsText(fp, filter);
        }

        public FileInfo GetAssociatedLyricsFile()
        {
            var inSameFolder = Path.ChangeExtension(mediaFileName, "lyrics.txt");
            if (File.Exists(inSameFolder))
            {
                return new FileInfo(inSameFolder);
            }

            FileInfo f = new FileInfo(mediaFileName);
            var m = regexGetLectureAndPart.Match(f.Name);
            if (!m.Success)
                return null;
            var L = m.Groups[1].Value;
            var P = m.Groups[2].Value;
            var path = Path.Combine(transcriptsFolder, $@"L{L}\");

            DirectoryInfo d = new DirectoryInfo(path);
            if (!d.Exists)
                return null;
            var fp = d.GetFiles($"L{L}P{P}*.lyrics.txt").FirstOrDefault();
            return fp;
        }

        internal static string GetLyricsTimestamp(int playerPositionMilliseconds)
        {
            int seconds = playerPositionMilliseconds / 1000;
            int milliseconds = playerPositionMilliseconds % 1000;
            int minutes = seconds / 60;
            seconds = seconds % 60;

            return $"[{minutes:D3}:{seconds:D2}.{milliseconds:D3}] ";
        }

        internal static string GetImagesTimestamp(int playerPositionMilliseconds)
        {
            int seconds = playerPositionMilliseconds / 1000;
            int minutes = seconds / 60;
            seconds = seconds % 60;
            if (minutes > 0)
                return minutes.ToString() + seconds.ToString("D2");
            return seconds.ToString();
        }

        public static IEnumerable<string> GetLyricsText(FileInfo sourceLyricsFile, string filter)
        {
            if (!sourceLyricsFile.Exists)
                yield break;

            using (var fr = sourceLyricsFile.OpenText())
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    if (filter != "")
                    {
                        bool val = DefaultTextMatch(filter, line);
                        if (val)
                            yield return line;
                    }
                    else
                        yield return line;
                }
            }
        }

        static Regex re = null;

        static string lastFilterString = string.Empty;

        internal static bool DefaultTextMatch(string filter, string text)
        {
            if (filter != lastFilterString)
            {
                re = null;
                re = new Regex(filter, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                lastFilterString = filter;
            }
            if (re == null)
                return CultureInfo.CurrentCulture.CompareInfo.IndexOf(text, filter, CompareOptions.IgnoreCase) >= 0;
            return re.IsMatch(text);
        }

        Regex regexGetLectureAndPart { get; } = new Regex(@"L(\d+)P(\d+)", RegexOptions.Compiled); // TODO: Remove from here, and move to session class

        internal SyncViewRepository(Track track)
        {
            mediaFileName = track.NowPlayingTrackInfo.FilePath.LocalPath;
            ReloadImages();
            ReloadPointers();
        }

        internal static IEnumerable<FileInfo> GetTranscripts()
        {
            DirectoryInfo d = new DirectoryInfo(transcriptsFolder);
            if (!d.Exists)
                return Enumerable.Empty<FileInfo>();
            return d.GetFiles("*.lyrics.txt", System.IO.SearchOption.AllDirectories);
        }
      

        internal void ReloadImages()
        {
            var d = GetImagePath();
            if (d == null)
            {
                Images = new TimedObjects<ImageInfo>();
                return;
            }
            var timedImages = d?.GetFiles("*.png").Select(x => new ImageInfo(x)).ToList();
            if (timedImages.Any(x => x.TimeStampMilliseconds == -1))
            {
                var minDateTime = timedImages.Min(x => x.file.CreationTime);
                var basicTime = GetBasicTime(minDateTime);

                var imagestoChange = timedImages.Where(x => x.TimeStampMilliseconds == -1).ToArray();
                foreach (var item in imagestoChange)
                {
                    var tmp = item.file.CreationTime - basicTime;
                    item.TimeStampMilliseconds = (int)tmp.TotalMilliseconds;
                }
            }
            Images = new TimedObjects<ImageInfo>(timedImages.OrderBy(x => x.TimeStampMilliseconds).ToList());
        }

        private DirectoryInfo GetImagePath()
        {
            FileInfo f = new FileInfo(mediaFileName);
            var lyr = GetAssociatedLyricsFile();
            if (lyr != null && lyr.Directory.FullName == f.Directory.FullName)
            {
                // it's likely that all images are in the same folder.
                return f.Directory;
            }
            var m = regexGetLectureAndPart.Match(f.Name);
            if (!m.Success)
                return null;
            var L = m.Groups[1].Value;
            var P = m.Groups[2].Value;
            DirectoryInfo d = GetImagePath(L, P);
            return d;
        }

        private void ReloadPointers()
        {
            Pointers = new TimedObjects<PointerCoordinates>();
            var d = GetImagePath();
            
            if (d == null || !d.Exists)
                return;
            var path = Path.Combine(d.FullName, "cursor.txt");
            FileInfo f = new FileInfo(path);
            if (!f.Exists)
                return;

            var l = new List<PointerCoordinates>();
            foreach (var line in File.ReadLines(f.FullName))
            {
                var pc = PointerCoordinates.FromString(line);
                if (pc != null)
                    l.Add(pc);
            }
            Pointers = new TimedObjects<PointerCoordinates>(l);
        }


        private DateTime GetBasicTime(DateTime defaultTime)
        {
            FileInfo f = new FileInfo(mediaFileName);
            Match m;
            Regex rTime = new Regex(@"_(\d\d)-(\d\d)-(\d\d)\.mp3");
            m = rTime.Match(f.Name);
            if (m.Success)
            {
                var day = defaultTime.Date; // same day, different time
                var hours = Convert.ToInt32(m.Groups[1].Value);
                var minutes = Convert.ToInt32(m.Groups[2].Value);
                var seconds = Convert.ToInt32(m.Groups[3].Value);
                return day.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
            }
            return defaultTime;
        }

        internal IEnumerable<Bookmark> GetTranscriptBookmarks(string sought)
        {
            var f = GetAssociatedLyricsFile();
            if (f == null)
                yield break;
            var curSession = Session.FromTranscriptFile(f.Name);
            var thisList = SyncViewRepository.GetLyricsText(f, sought).ToList();
            foreach (var transcrriptLine in thisList)
            {
                Bookmark b = new Bookmark();
                b.session = curSession;
                b.Text = transcrriptLine;
                b.Timing = SyncViewRepository.GetMilli(transcrriptLine);
                b.Type = Bookmark.SourceType.Transcript;
                yield return b;
            }
        }

        public static string SupportingMediaFolder = @"C:\Data\Work\Esame Stato\SupportingMedia";

        private static DirectoryInfo GetImagePath(string L, string P)
        {
            DirectoryInfo d = new DirectoryInfo(Path.Combine(SupportingMediaFolder, $@"L{L}\P{P}\"));
            if (d.Exists)
                return d;
            // see if there's a temporary path for images:
            d = new DirectoryInfo(Path.Combine(SupportingMediaFolder, $@"T{L}\P{P}\"));
            if (d.Exists)
                return d;
            return null;
        }

        internal static Regex regexGetLyricsTime = new Regex(@"^\[(\d+):(\d+)\.(\d+)] ", RegexOptions.Compiled);

        internal static int GetMilli(string txt)
        {
            var m = regexGetLyricsTime.Match(txt);
            if (!m.Success)
                return -1;
            int min = Convert.ToInt32(m.Groups[1].Value);
            int sec = Convert.ToInt32(m.Groups[2].Value);
            int mill = Convert.ToInt32(m.Groups[3].Value);

            // System.Diagnostics.Debug.WriteLine($"{min} {sec} {mill}");

            sec += min * 60;
            mill += sec * 1000;
            return mill;
        }

        internal FileInfo GetLastImage()
        {
            if (Images.LastObjectPolled != -1)
            {
                return Images[Images.LastObjectPolled].file;
            }
            return null;
        }

        internal bool TrySetImageName(string text)
        {
            var success = false;
            if (Images.LastObjectPolled != -1)
            {
                success = Images[Images.LastObjectPolled].TrySetImageName(text);
            }
            if (success)
                ReloadImages();
            return success;
        }

        internal bool DeleteCurrentImage()
        {
            var success = false;
            if (Images.LastObjectPolled != -1)
            {
                var fname = Images[Images.LastObjectPolled].file;
                FileSystem.DeleteFile(fname.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                success = true;
            }
            if (success)
                ReloadImages();
            return success;
        }

        static internal IEnumerable<Bookmark> FindImagesAllRepos(string sought)
        {
            var dir = new DirectoryInfo(SupportingMediaFolder);
            if (!dir.Exists)
                yield break;
            foreach (var file in dir.GetFiles("*.png", System.IO.SearchOption.AllDirectories))
            {
                var book = Bookmark.FromImageFile(file);
                if (book == null)
                    continue;
                if (DefaultTextMatch(sought, book.Text))
                {
                    yield return book;
                }
            }
        }
        internal IEnumerable<Bookmark> GetImagesBookmarks(string sought)
        {
            if (Images == null)
                yield break;
            foreach (var img in Images)
            {
                var book = Bookmark.FromImageFile(img.file);
                if (book == null)
                    continue;
                if (DefaultTextMatch(sought, book.Text))
                {
                    yield return book;
                }
            }
        }
    }
}
