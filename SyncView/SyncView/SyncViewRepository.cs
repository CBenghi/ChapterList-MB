using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChapterListMB.SyncView
{
    partial class SyncViewRepository
    {
        internal string mediaFileName { get; private set; }
        // FileInfo[] images;
        // int[] timesInMilliseconds;

        internal TimedObjects<ImageInfo> Images;
        
        internal TimedObjects<PointerCoordinates> Pointers;

        private static string transcriptsFolder = @"C:\Data\Work\Esame Stato\SupportingMedia\Transcripts\";

        private static string audioFolder = @"C:\Data\Work\Esame Stato\Audio";

        public IEnumerable<string> GetLyricsText(string filter)
        {
            FileInfo f = new FileInfo(mediaFileName);
            
            var m = regexGetLectureAndPart.Match(f.Name);
            if (!m.Success)
                return Enumerable.Empty<string>();
            var L = m.Groups[1].Value;
            var P = m.Groups[2].Value;
            var path = Path.Combine(transcriptsFolder, $@"L{L}\");

            DirectoryInfo d = new DirectoryInfo(path);

            var fp = d.GetFiles($"L{L}P{P}*.lyrics.txt").FirstOrDefault();
            if (!fp.Exists)
                return Enumerable.Empty<string>();

            return GetLyricsText(fp, filter);
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

        public static IEnumerable<string> GetLyricsText(FileInfo fp, string filter)
        {
            if (!fp.Exists)
                yield break;

            using (var fr = fp.OpenText())
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    if (filter != "")
                    {
                        if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(line, filter, CompareOptions.IgnoreCase) >= 0)
                            yield return line;
                    }
                    else
                        yield return line;
                }
            }
        }

        Regex regexGetLectureAndPart = new Regex(@"L(\d+)P(\d+)", RegexOptions.Compiled);

        internal SyncViewRepository(Track track)
        {
            Debug.WriteLine(track.NowPlayingTrackInfo.FilePath.LocalPath);
            mediaFileName = track.NowPlayingTrackInfo.FilePath.LocalPath;
            ReloadImages();
            ReloadPointers();
        }

        

        internal static IEnumerable<FileInfo> GetTranscripts()
        {
            DirectoryInfo d = new DirectoryInfo(transcriptsFolder);
            if (!d.Exists)
                return Enumerable.Empty<FileInfo>();
            return d.GetFiles("*.lyrics.txt", SearchOption.AllDirectories);
        }

        internal FileInfo AudioFromTrascriptName(string transcriptFileName)
        {
            var m = regexGetLectureAndPart.Match(transcriptFileName);
            
            if (!m.Success)
            {
                return null;
            }
            var L = m.Groups[1].Value;
            var P = m.Groups[2].Value;
            var path = Path.Combine(audioFolder, $"L{L}");

            DirectoryInfo d = new DirectoryInfo(path);
            return d.GetFiles($"L{L}P{P}*.mp3").FirstOrDefault();
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

        private static DirectoryInfo GetImagePath(string L, string P)
        {
            DirectoryInfo d = new DirectoryInfo($@"C:\Data\Work\Esame Stato\SupportingMedia\L{L}\P{P}\");
            if (d.Exists)
                return d;
            // see if there's a temporary path for images:
            d = new DirectoryInfo($@"C:\Data\Work\Esame Stato\SupportingMedia\T{L}\P{P}\");
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
                fname.Delete();
                success = true;
            }
            if (success)
                ReloadImages();
            return success;
        }
    }
}
