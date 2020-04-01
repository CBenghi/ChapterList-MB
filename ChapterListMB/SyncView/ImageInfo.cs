using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ChapterListMB.SyncView
{

    class ImageInfo : ITimedObject
    {
        Regex rPositionPart = new Regex(@"^(\d+)[ ]*-", RegexOptions.Compiled);

        internal FileInfo file { get; set; }
        public int TimeStampMilliseconds { get; set; } = -1;

        public override string ToString()
        {
            var fname = file.Name;
            return fname;
        }

        internal ImageInfo(FileInfo file)
        {
            this.file = file;
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
                TimeStampMilliseconds = seconds * 1000;
            }
        }

        internal bool TrySetImageName(string text)
        {
            if (!file.Exists)
                return false;

            var ext = file.Extension.Substring(1);

            var fileName = $"{text}.{ext}";
            if (TimeStampMilliseconds != -1)
            {
                fileName = $"{SyncViewRepository.GetImagesTimestamp(TimeStampMilliseconds)} - {text}.{ext}";
            }
            var newName = Path.Combine(file.DirectoryName, fileName);
            if (File.Exists(newName))
                return false;
            file.MoveTo(newName);
            return true;
        }

        internal string GetName()
        {
            var name = Path.GetFileNameWithoutExtension(file.Name);
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
            if (!file.Exists)
                return false;

            var ext = file.Extension.Substring(1);
            var text = GetName();
            var fileName = $"{text}.{ext}";
            if (lastTimeMilli != -1)
            {
                fileName = $"{SyncViewRepository.GetImagesTimestamp(lastTimeMilli)} - {text}.{ext}";
            }
            var newName = Path.Combine(file.DirectoryName, fileName);
            if (File.Exists(newName))
                return false;
            file.MoveTo(newName);
            return true;
        }
    }
}
