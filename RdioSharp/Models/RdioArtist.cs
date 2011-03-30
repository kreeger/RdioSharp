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

        private RdioArtist(string key, string name, string url, string icon, string baseIcon, RdioType rdioType,
                           int trackCount, bool hasRadio, string shortUrl, int albumCount = -1)
        {
            Key = key;
            Name = name;
            Url = url;
            Icon = icon;
            BaseIcon = baseIcon;
            RdioType = rdioType;
            Name = name;
            TrackCount = trackCount;
            HasRadio = hasRadio;
            ShortUrl = shortUrl;
            AlbumCount = albumCount;
        }

        public RdioArtist(IDictionary<string, object> dictionary)
        {
            Key = (string)dictionary["key"];
            Url = (string)dictionary["url"];
            Icon = (string)dictionary["icon"];
            BaseIcon = (string)dictionary["baseIcon"];
            RdioType = (RdioType)dictionary["rdioType"];
            Name = (string)dictionary["name"];
            TrackCount = (int)dictionary["length"];
            HasRadio = (bool)dictionary["hasRadio"];
            ShortUrl = (string)dictionary["shortUrl"];
            object albumCount;
            if (dictionary.TryGetValue("albumCount", out albumCount))
                AlbumCount = (int)albumCount;
        }
    }
}
