﻿using System;

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

        private RdioPlaylist(string key, string url, string icon, string baseIcon, RdioType rdioType,
                        string name, int trackCount, string ownerName, string ownerUrl, string ownerKey,
                        string ownerIcon, DateTime lastUpdated, string shortUrl, string embedUrl)
        {
            Key = key;
            Name = name;
            Url = url;
            Icon = icon;
            BaseIcon = baseIcon;
            RdioType = rdioType;
            TrackCount = trackCount;
            OwnerName = ownerName;
            OwnerUrl = ownerUrl;
            OwnerKey = ownerKey;
            OwnerIcon = ownerIcon;
            LastUpdated = lastUpdated;
            ShortUrl = shortUrl;
            EmbedUrl = embedUrl;
        }
    }
}