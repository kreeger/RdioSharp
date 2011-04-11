using RdioSharp.Enum;

namespace RdioSharp.Models
{
    public class RdioArtist : IRdioObject
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string BaseIcon { get; set; }
        public RdioType RdioType { get; set; }
        public int TrackCount { get; set; }
        public bool HasRadio { get; set; }
        public string ShortUrl { get; set; }
        public int AlbumCount { get; set; }

        internal RdioArtist() { }
    }
}
