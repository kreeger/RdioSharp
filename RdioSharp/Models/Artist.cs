namespace RdioSharp.Models
{
    public class Artist : RdioObject
    {
        public string Name { get; private set; }
        public int TrackCount { get; private set; }
        public bool HasRadio { get; private set; }
        public string ShortUrl { get; private set; }
        public int AlbumCount { get; private set; }

        /// <summary>
        /// NOTE: ADD INITIALIZER FOR DICT, TOO; ALSO, PARAM DOCS
        /// </summary>
        /// <param name="key"></param>
        /// <param name="url"></param>
        /// <param name="icon"></param>
        /// <param name="baseIcon"></param>
        /// <param name="rdioType"></param>
        /// <param name="name"></param>
        /// <param name="trackCount"></param>
        /// <param name="hasRadio"></param>
        /// <param name="shortUrl"></param>
        /// <param name="albumCount"></param>
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
