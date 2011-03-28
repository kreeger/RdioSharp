using System;
using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioTrack : IRdioMusicObject
    {
        public string Key { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public string Icon { get; private set; }
        public string BaseIcon { get; private set; }
        public RdioType RdioType { get; private set; }
        public string ArtistName { get; private set; }
        public string ArtistUrl { get; private set; }
        public string ArtistKey { get; private set; }
        public bool IsExplicit { get; private set; }
        public bool IsClean { get; private set; }
        public decimal Price { get; private set; }
        public bool CanStream { get; private set; }
        public bool CanSample { get; private set; }
        public bool CanTether { get; private set; }
        public string ShortUrl { get; private set; }
        public string EmbedUrl { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string AlbumArtistName { get; private set; }
        public string AlbumArtistKey { get; private set; }
        public bool CanDownload { get; private set; }
        public bool CanDownloadAlbumOnly { get; private set; }
        public int PlayCount { get; private set; }

        private RdioTrack(string key, string url, string icon, string baseIcon, RdioType rdioType,
            string name, string artistName, string artistUrl, string artistKey, bool isExplicit, bool isClean,
            decimal price, bool canStream, bool canSample, bool canTether, string shortUrl, string embedUrl,
            TimeSpan duration, string albumArtistName, string albumArtistKey, bool canDownload,
            bool canDownloadAlbumOnly, int playCount = -1)
        {
            Key = key;
            Url = url;
            Icon = icon;
            BaseIcon = baseIcon;
            RdioType = rdioType;
            Name = name;
            ArtistName = artistName;
            ArtistUrl = artistUrl;
            ArtistKey = artistKey;
            IsExplicit = isExplicit;
            IsClean = isClean;
            Price = price;
            CanStream = canStream;
            CanSample = canSample;
            CanTether = canTether;
            ShortUrl = shortUrl;
            EmbedUrl = embedUrl;
            Duration = duration;
            AlbumArtistName = albumArtistName;
            AlbumArtistKey = albumArtistKey;
            CanDownload = canDownload;
            CanDownloadAlbumOnly = canDownloadAlbumOnly;
            PlayCount = playCount;
        }

        public RdioTrack(IDictionary<string, object> dictionary)
        {
            Key = (string)dictionary["key"];
            Url = (string)dictionary["url"];
            Icon = (string)dictionary["icon"];
            BaseIcon = (string)dictionary["baseIcon"];
            RdioType = (RdioType)dictionary["rdioType"];
            Name = (string)dictionary["name"];
            ArtistName = (string)dictionary["artistName"];
            ArtistUrl = (string)dictionary["artistUrl"];
            ArtistKey = (string)dictionary["artistKey"];
            IsExplicit = (bool)dictionary["isExplicit"];
            IsClean = (bool)dictionary["isClean"];
            Price = (decimal)dictionary["price"];
            CanStream = (bool)dictionary["canStream"];
            CanSample = (bool)dictionary["canSample"];
            CanTether = (bool)dictionary["canTether"];
            ShortUrl = (string)dictionary["shortUrl"];
            EmbedUrl = (string)dictionary["embedUrl"];
            Duration = (TimeSpan)dictionary["duration"];
            AlbumArtistName = (string)dictionary["albumArtistName"];
            AlbumArtistKey = (string)dictionary["albumArtistKey"];
            CanDownload = (bool)dictionary["canDownload"];
            CanDownloadAlbumOnly = (bool)dictionary["canDownloadAlbumOnly"];
            object playCount;
            if (dictionary.TryGetValue("trackKeys", out playCount))
                PlayCount = (int)playCount;
        }
    }
}
