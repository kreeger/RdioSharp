using System;

using RdioSharp.Enum;

namespace RdioSharp.Models
{
    public class RdioPlaylist: IRdioObject
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string BaseIcon { get; set; }
        public RdioType RdioType { get; set; }
        public int TrackCount { get; set; }
        public string OwnerName { get; set; }
        public string OwnerUrl { get; set; }
        public string OwnerKey { get; set; }
        public string OwnerIcon { get; set; }
        public DateTime LastUpdated { get; set; }
        public string ShortUrl { get; set; }
        public string EmbedUrl { get; set; }

        internal RdioPlaylist() { }
    }
}
