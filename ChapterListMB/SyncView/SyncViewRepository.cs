using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        int lastTimeReceived = -1;
        int lastImagePlayed = -1;
        int nextImageThreshold = -1;

        internal string mediaFileName { get; private set; }

        FileInfo[] images;
        int[] timesInMilliseconds;

        public int getNextImageMilli()
        {
            return nextImageThreshold;
        }

        public int getImageIndex(int seekTime)
        {
            if (images == null || !images.Any())
                return -1;

            // if we moved back start afresh                
            if (seekTime < lastTimeReceived || lastTimeReceived == -1)
                return FindAtTime(seekTime);
            if (seekTime < nextImageThreshold)
            {
                lastTimeReceived = seekTime;
                return -1; // nothing to do
            }
            return FindAtTime(seekTime, lastImagePlayed + 1);
        }

        private int FindAtTime(int seekTime, int startAt = 0)
        {
            lastTimeReceived = seekTime;
            int ret = 0; // in any case return first image, if too early
            for (int i = startAt; i < timesInMilliseconds.Length; i++)
            {
                if (seekTime >= timesInMilliseconds[i])
                {
                    ret = i;
                }
                else
                {
                    break;
                }
            }
            lastImagePlayed = ret;
            if (ret < timesInMilliseconds.Length - 1)
                nextImageThreshold = timesInMilliseconds[ret + 1];
            else
                nextImageThreshold = int.MaxValue;
            return ret;
        }

        private static string transcriptsFolder = @"C:\Data\Work\Esame Stato\SupportingMedia\Transcripts\";

        private static string audioFolder = @"C:\Data\Work\Esame Stato\Audio";

        public IEnumerable<string> GetLyricsText(string filter)
        {
            FileInfo f = new FileInfo(mediaFileName);
            
            var m = regexGetLectureAndPart.Match(f.Name);
            if (!m.Success)
                return Enumerable.Empty<string>();
            var L = m.Groups[1].Value;
            var path = Path.Combine(transcriptsFolder, $@"L{L}\");

            DirectoryInfo d = new DirectoryInfo(path);
            path = Path.Combine(path, f.Name);
            path = Path.ChangeExtension(path, ".lyrics.txt");

            FileInfo fp = new FileInfo(path);
            return GetLyricsText(fp, filter);
        }

        internal string GetLyricsTimestamp(int playerPositionMilliseconds)
        {
            int seconds = playerPositionMilliseconds / 1000;
            int milliseconds = playerPositionMilliseconds % 1000;
            int minutes = seconds / 60;
            seconds = seconds % 60;

            return $"[{minutes:D3}:{seconds:D2}.{milliseconds:D3}] ";
        }

        internal string GetImagesTimestamp(int playerPositionMilliseconds)
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
        }

        internal FileInfo GetiImageFile(int imageIndex)
        {
            return images[imageIndex];
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
            ResetInit();

            FileInfo f = new FileInfo(mediaFileName);
            var m = regexGetLectureAndPart.Match(f.Name);
            if (!m.Success)
                return;

            var L = m.Groups[1].Value;
            var P = m.Groups[2].Value;
            DirectoryInfo d = GetImagePath(L, P);
            if (d == null)
                return;

            // images = d.GetFiles("*.png").OrderBy(x => x.CreationTime).ToArray();
            var timedImages = d.GetFiles("*.png").Select(x => new ImageInfo(x)).ToList();

            if (timedImages.Any(x => x.computedTimeStampMilliseconds == -1))
            {
                var minDateTime = timedImages.Min(x => x.f.CreationTime);

                var basicTime = GetBasicTime(minDateTime);
                for (int i = 1; i < timedImages.Count; i++)
                {
                    if (timedImages[i].computedTimeStampMilliseconds == -1)
                    {
                        var tmp = timedImages[i].f.CreationTime - basicTime;
                        timedImages[i].computedTimeStampMilliseconds = (int)tmp.TotalMilliseconds;
                    }
                }
            }
            var sorted = timedImages.OrderBy(x => x.computedTimeStampMilliseconds);
            images = sorted.Select(x => x.f).ToArray();
            timesInMilliseconds = sorted.Select(x => x.computedTimeStampMilliseconds).ToArray();
        }

        private void ResetInit()
        {
            lastTimeReceived = -1;
            lastImagePlayed = -1;
            nextImageThreshold = -1;
            images = new FileInfo[0];
            timesInMilliseconds = new int[0];
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
            if (lastImagePlayed != -1)
            {
                // return images[lastImagePlayed].TrySetImageName(text);
            }
            return false;
        }
    }
}
