using System;
using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioAlbum : IRdioMusicObject
    {
        public string Key { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public string Icon { get; private set; }
        public string BaseIcon { get; private set; }
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
        public RdioType RdioType { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public IList<string> TrackKeys { get; private set; }

        private RdioAlbum(string key, string url, string icon, string baseIcon, RdioType rdioType,
            string name, string artistName, string artistUrl, string artistKey, bool isExplicit, bool isClean,
            decimal price, bool canStream, bool canSample, bool canTether, string shortUrl, string embedUrl,
            TimeSpan duration, DateTime releaseDate, IList<string> trackKeys = null)
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
            ReleaseDate = releaseDate;
            TrackKeys = trackKeys;
        }
    }
}
