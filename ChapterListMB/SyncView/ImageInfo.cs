using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ChapterListMB.SyncView
{

    class ImageInfo
    {
        Regex rPositionPart = new Regex(@"^(\d+)[ ]*-", RegexOptions.Compiled);

        internal FileInfo f { get; set; }
        internal int computedTimeStampMilliseconds { get; set; } = -1;

        public override string ToString()
        {
            var fname = f.Name;
            return fname;
        }

        internal ImageInfo(FileInfo file)
        {
            f = file;
            var m = rPositionPart.Match(file.Name);
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

        internal bool TrySetImageName(string text)
        {
            if (!f.Exists)
                return false;

            var ext = f.Extension.Substring(1);

            var fileName = $"{text}.{ext}";
            if (computedTimeStampMilliseconds != -1)
            {
                fileName = $"{SyncViewRepository.GetImagesTimestamp(computedTimeStampMilliseconds)} - {text}.{ext}";
            }
            var newName = Path.Combine(f.DirectoryName, fileName);
            if (File.Exists(newName))
                return false;
            f.MoveTo(newName);
            return true;
        }

        internal string GetName()
        {
            var name = Path.GetFileNameWithoutExtension(f.Name);
            var m = rPositionPart.Match(name);
            if (m.Success)
            {
                name = name.Replace(m.Value, "");
            }
            name = name.Trim();
            return name;
        }

        internal bool TrySetImageTime(int lastTimeMilli)
        {
            if (!f.Exists)
                return false;

            var ext = f.Extension.Substring(1);
            var text = GetName();
            var fileName = $"{text}.{ext}";
            if (lastTimeMilli != -1)
            {
                fileName = $"{SyncViewRepository.GetImagesTimestamp(lastTimeMilli)} - {text}.{ext}";
            }
            var newName = Path.Combine(f.DirectoryName, fileName);
            if (File.Exists(newName))
                return false;
            f.MoveTo(newName);
            return true;
        }
    }
}
