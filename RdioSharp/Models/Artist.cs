namespace RdioSharp.Models
{
    public class Artist : RdioObject
    {
        public string Name { get; private set; }
        public int TrackCount { get; private set; }
        public bool HasRadio { get; private set; }
        public string ShortUrl { get; private set; }
        public int AlbumCount { get; private set; }

        internal Artist(string key, string url, string icon, string baseIcon, RdioType rdioType,
                        string name, int trackCount, bool hasRadio, string shortUrl, int albumCount = 0)
            : base(key, url, icon, baseIcon, rdioType)
        {
            Name = name;
            TrackCount = trackCount;
            HasRadio = hasRadio;
            ShortUrl = shortUrl;
            AlbumCount = albumCount;
        }
    }
}
