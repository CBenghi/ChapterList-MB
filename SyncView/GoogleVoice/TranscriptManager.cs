using ChapterListMB.SyncView;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncView
{
    class TranscriptManager
    {
        public int WordSeparationThreshold { get; set; } = 500;

        public static LongRunningRecognizeResponse Read(string filename)
        {
            var file = new FileInfo(filename);
            using (var rf = file.OpenText())
            {
                var all = rf.ReadToEnd();
                return LongRunningRecognizeResponse.Parser.ParseJson(all);
            }
        }

        public bool overwrite { get; set; } = true;

        internal string MakeLyrics(bool timeStamp = true)
        {
            string lyricsContent = "no new files in transcript folder.";
            var d = new DirectoryInfo(@"C:\Data\Work\Esame Stato\SupportingMedia\Transcripts\");
            foreach (var item in d.GetFiles("*.json", SearchOption.AllDirectories))
            //    foreach (var item in SpeechConvert.GetFiles("*.json"))
            {
                var outFileName = Path.ChangeExtension(item.FullName, ".lyrics.txt");
                if (!overwrite && File.Exists(outFileName))
                    continue;
                lyricsContent = MakeLyrics(item, timeStamp, outFileName);
            }
            return lyricsContent;
        }

        internal string MakeLyrics(FileInfo item, bool writeTimeStamps = true, string outFileName = "")
        {
            if (outFileName == "")
            {
                outFileName = Path.ChangeExtension(item.FullName, ".lyrics.txt");
            }
            string lyricsContent;
            var speechResponse = Read(item.FullName);
            lyricsContent = MakeLyrics(speechResponse, WordSeparationThreshold, writeTimeStamps);
            FileInfo f = new FileInfo(outFileName);
            using (var fw = f.CreateText())
            {
                fw.Write(lyricsContent);
            }

            return lyricsContent;
        }

        internal static string MakeLyrics(LongRunningRecognizeResponse speechResponse, int WordSeparationThreshold, bool writeTimeStamps = false)
        {
            var t = new TimeSpan(0, 0, 0, 0, WordSeparationThreshold);

            StringBuilder sb = new StringBuilder();
            int minWordCountOnLine = 7;
            int maxWordCountOnLine = 15;

            int written = 0;
            foreach (var result in speechResponse.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    foreach (var wordInformation in alternative.Words)
                    {
                        wordInformation.Word = FixWordException(wordInformation.Word);

                        var d = wordInformation.LenghtTimeSpan();
                        if (
                            (d > t && written >= minWordCountOnLine)
                            ||
                            (written > maxWordCountOnLine)
                            )
                        {
                            sb.Append(Environment.NewLine);
                            written = 0;
                        }
                        else if (!isNewLine(written))
                        {
                            sb.Append(" ");
                        }
                        if (isNewLine(written) && writeTimeStamps)
                        {
                            sb.Append(SyncViewRepository.GetLyricsTimestamp(wordInformation.StartTimeMilliseconds()));
                        }
                        sb.Append(wordInformation.Word);
                        written++;
                        if (EndsWithPunctuation(wordInformation.Word))
                        {
                            sb.Append(Environment.NewLine);
                            written = 0;
                        }
                    }
                    // sb.Append(Environment.NewLine);
                }
            }
            var tmp = sb.ToString();
            return tmp;
        }

        private static bool EndsWithPunctuation(string word)
        {
            word = word.Trim();
            var lastchar = word.Last();
            // return char.IsPunctuation(lastchar);
            switch (lastchar)
            {
                case ',':
                    return true;
                case '.':
                    return true;
                default:
                    return false;
            }
        }

        private static string FixWordException(string word)
        {
            if (word == "cm.")
                return "cm";
            return word;
        }

        private static bool isNewLine(int written)
        {
            return written == 0;
        }
    }
}
