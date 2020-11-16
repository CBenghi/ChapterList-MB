using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MusicBeePlugin;

namespace ChapterListMB
{
    public class CLTrack : IEquatable<CLTrack>
    {
        public NowPlayingTrackInfo NowPlayingTrackInfo { get; set; }
        public static Uri XmlPath;
        public CLTrack(NowPlayingTrackInfo trackInfo)
        {
            XmlPath = new Uri($"{trackInfo.FilePath}.xml", UriKind.Absolute);
            NowPlayingTrackInfo = trackInfo;
        }

        public override string ToString()
        {
            return $"{NowPlayingTrackInfo.Artist} - {NowPlayingTrackInfo.Album} - {NowPlayingTrackInfo.Title}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            CLTrack track  = obj as CLTrack;
            if ((object) track == null)
                return false;
            return NowPlayingTrackInfo.Equals(track.NowPlayingTrackInfo);
        }
        public bool Equals(CLTrack other)
        {
            if ((object) other == null) return false;
                return NowPlayingTrackInfo.Equals(other.NowPlayingTrackInfo);
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
