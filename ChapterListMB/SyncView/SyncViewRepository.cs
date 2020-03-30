using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChapterListMB.SyncView
{
    class SyncViewRepository
    {
        int lastTimeReceived = -1;
        int lastImagePlayed = -1;
        int nextImageThreshold = -1;

        internal string mediaFileName { get; private set; }

        FileInfo[] images;
        int[] timesInMilliseconds;

        public int getImageIndex(int seekTime)
        {
            if (images == null || !images.Any())
                return -1;

            // if we moved back start afresh                
            if (seekTime < lastTimeReceived)
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
                        if (line.Contains(filter))
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

            FileInfo f = new FileInfo(track.NowPlayingTrackInfo.FilePath.LocalPath);
            
            var m = regexGetLectureAndPart.Match(f.Name);
            if (m.Success)
            {
                var L = m.Groups[1].Value;
                var P = m.Groups[2].Value;
                var path = $@"C:\Data\Work\Esame Stato\SupportingMedia\L{L}\P{P}\";

                DirectoryInfo d = new DirectoryInfo(path);
                if (!d.Exists)
                    return;
                images = d.GetFiles("*.png").OrderBy(x => x.CreationTime).ToArray();
            }
            if (!images.Any())
                return;
            timesInMilliseconds = new int[images.Length];

            var basicTime = images[0].CreationTime;
            Regex rTime = new Regex(@"_(\d\d)-(\d\d)-(\d\d)\.mp3");
            m = rTime.Match(f.Name);
            if (m.Success)
            {
                var day = basicTime.Date; // same day, different time
                var hours = Convert.ToInt32(m.Groups[1].Value);
                var minutes = Convert.ToInt32(m.Groups[2].Value);
                var seconds = Convert.ToInt32(m.Groups[3].Value);
                basicTime = day.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
            }

            for (int i = 1; i < images.Length; i++)
            {
                var tmp = images[i].CreationTime - basicTime;
                timesInMilliseconds[i] = (int)tmp.TotalMilliseconds;
            }
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
    }
}
