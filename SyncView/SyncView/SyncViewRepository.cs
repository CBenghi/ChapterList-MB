using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
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
        public SyncViewRepository()
        {

        }

        public enum FileLocation
        {
            MediaFollowsAudio,
            SeparateSupportingMedia
        }

        public FileLocation FileLocationMode { get; set; }


        internal string mediaFileName { get; private set; }
        
        internal TimedObjects<ImageInfo> Images;
        
        internal TimedObjects<PointerCoordinates> Pointers;

        /// <summary>
        /// This changes if you have a SyncView.json file in the directory tree of the selected media.
        /// </summary>
        private DirectoryInfo SupportingMediaFolder { get; set; } = new DirectoryInfo(@"C:\Data\Work\Esame Stato\SupportingMedia");

        public DirectoryInfo AudioFolder { get; set; } = new DirectoryInfo(@"C:\Data\Work\Esame Stato\Audio");

        private string transcriptsFolder
        {
            get
            {
                if (FileLocationMode == FileLocation.SeparateSupportingMedia)
                    return Path.Combine(SupportingMediaFolder.FullName, "Transcripts");
                return RepoRootFolder.FullName;
            }
        }
                

        private void LocateRepoRoot()
        {
            FileInfo f = new FileInfo(mediaFileName);
            var d = f.Directory;
            while (d != null && d.Exists)
            {
                var ini = d.GetFiles("SyncView.json").FirstOrDefault();
                if (ini != null && ini.Exists)
                {
                    var content = File.ReadAllText(ini.FullName);
                    var deserializedProduct = JsonConvert.DeserializeObject<RepoPersistence>(content);
                    deserializedProduct.SetJsonFolder(d);
                    if (deserializedProduct.FileLocationMode == FileLocation.SeparateSupportingMedia)
                    {
                        SupportingMediaFolder = deserializedProduct.ResolvedSupportingMediaFolder;
                        AudioFolder = deserializedProduct.ResolvedAudioFolder;
                    }
                    else 
                        RepoRootFolder = d;
                    FileLocationMode = deserializedProduct.FileLocationMode;
                    return;
                }
                d = d.Parent;
            }
        }

        public class RepoPersistence
        {
            private string supportingMediaFolder;
            public string SupportingMediaFolder
            {
                get => supportingMediaFolder; 
                set
                {
                    FileLocationMode = FileLocation.SeparateSupportingMedia;
                    supportingMediaFolder = value;
                }
            }

            private string audioFolder;
            public string AudioFolder
            {
                get => audioFolder; 
                set
                {
                    FileLocationMode = FileLocation.SeparateSupportingMedia;
                    audioFolder = value;
                }
            }

            public FileLocation FileLocationMode { get; set; } = FileLocation.MediaFollowsAudio;

            DirectoryInfo persistenceFileFolder;
            

            internal void SetJsonFolder(DirectoryInfo d)
            {
                persistenceFileFolder = d;
            }

            internal DirectoryInfo ResolvedSupportingMediaFolder
            {
                get
                {
                    return ResolveFolder(SupportingMediaFolder);
                }
            }

            internal DirectoryInfo ResolvedAudioFolder
            {
                get
                {
                    return ResolveFolder(AudioFolder);
                }
            }

            private DirectoryInfo ResolveFolder(string directoryString)
            {
                if (Path.IsPathRooted(directoryString))
                {
                    return new DirectoryInfo(directoryString);
                }
                return new DirectoryInfo(
                    Path.Combine(persistenceFileFolder.FullName, directoryString)
                    );
            }
        }

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
            var fileFilter = $"L{L}P{P}*.lyrics.txt";
            var fp = d.GetFiles(fileFilter).FirstOrDefault();
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

        public static IEnumerable<string> GetLyricsText(FileInfo sourceLyricsFile, string filter, bool stripTiming = false)
        {
            
            if (!sourceLyricsFile.Exists)
                yield break;

            using (var fr = sourceLyricsFile.OpenText())
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var content = line;
                    if (stripTiming)
					{
                        content = Regexes.regexTranscriptTiming.Replace(content, "");
                    }
                    
                    if (filter != "")
                    {
                        bool val = DefaultTextMatch(filter, content);
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
        public DirectoryInfo RepoRootFolder { get; private set; }

        internal SyncViewRepository(CLTrack track)
        {
            mediaFileName = track.NowPlayingTrackInfo.FilePath.LocalPath;
            LocateRepoRoot();
            ReloadImages();
            ReloadPointers();
        }

        internal IEnumerable<FileInfo> GetTranscripts()
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

        internal DirectoryInfo GetImagePath()
        {
            FileInfo f = new FileInfo(mediaFileName);
            if (FileLocationMode == FileLocation.MediaFollowsAudio)
                return f.Directory;

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
                if (transcrriptLine.Contains("#skip"))
                    b.Type = Bookmark.SourceType.Skip;
                else             
                    b.Type = Bookmark.SourceType.Transcript;
                yield return b;
            }
        }

        private DirectoryInfo GetImagePath(string L, string P)
        {
            DirectoryInfo d = new DirectoryInfo(Path.Combine(SupportingMediaFolder.FullName, $@"L{L}\P{P}\"));
            if (d.Exists)
                return d;
            // see if there's a temporary path for images:
            d = new DirectoryInfo(Path.Combine(SupportingMediaFolder.FullName, $@"T{L}\P{P}\"));
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

        internal IEnumerable<Bookmark> FindImagesAllRepos(string sought)
        {
            
            if (!SupportingMediaFolder.Exists)
                yield break;
            foreach (var file in SupportingMediaFolder.GetFiles("*.png", System.IO.SearchOption.AllDirectories))
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

        internal void Save()
        {
            string output = JsonConvert.SerializeObject(this);
            Debug.WriteLine(output);
        }

        internal void AddSkip(int min, int max)
        {
            var same = (min == max);
            var f = GetAssociatedLyricsFile();
            var NeedAddNewLine = !FileEndsWithNewLine(f);
            using (var w = f.AppendText())
            {
                if (NeedAddNewLine)
                    w.WriteLine("");
                var pos = min - 250; // 250 milliseconds earlier than first line to skip
                var ts = new TimeSpan(0, 0, 0, 0, pos);
                int mins = (int)ts.TotalMinutes;
                int secss = ts.Seconds;
                int mlss = ts.Milliseconds;
                var m = $"[{mins:D3}:{secss:D2}.{mins:D3}] #skip";
                if (!same)
                    m = $"[{mins:D3}:{secss:D2}.{mins:D3}] #skip +{max - pos}";
                w.WriteLine(m);
            }
        }

		private bool FileEndsWithNewLine(FileInfo f)
		{
            if (f.Length < 2)
                return false;
            using (StreamReader sr = new StreamReader(f.FullName, encoding: System.Text.Encoding.UTF8))
            {
                //back 2 bytes from end of file
                sr.BaseStream.Seek(-2, SeekOrigin.End);

                int s1 = sr.Read(); //read the char before last
                int s2 = sr.Read(); //read the last char 
                if (s2 == 10) //file is end with CR-LF or LF ... (CR=13, LF=10)
                {
                    if (s1 == 13) {
                        return true;
                    } //file is end with CR-LF (Windows EOL format)
                    else if (s1 == 13)
                    { 
                        return true;
                    } //file is end with just LF, (UNIX/OSX format)
                    return false;
                }
                return false;
            }
        }
	}
}
