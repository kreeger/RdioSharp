using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioArtist : IRdioObject
    {
        public string Key { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public string Icon { get; private set; }
        public string BaseIcon { get; private set; }
        public RdioType RdioType { get; private set; }
        public int TrackCount { get; private set; }
        public bool HasRadio { get; private set; }
        public string ShortUrl { get; private set; }
        public int AlbumCount { get; private set; }

        internal RdioArtist() { }
    }
}
