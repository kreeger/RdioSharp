using System;
using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioPlaylist: IRdioObject
    {
        public string Key { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public string Icon { get; private set; }
        public string BaseIcon { get; private set; }
        public RdioType RdioType { get; private set; }
        public int TrackCount { get; private set; }
        public string OwnerName { get; private set; }
        public string OwnerUrl { get; private set; }
        public string OwnerKey { get; private set; }
        public string OwnerIcon { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public string ShortUrl { get; private set; }
        public string EmbedUrl { get; private set; }

        internal RdioPlaylist() { }
    }
}
