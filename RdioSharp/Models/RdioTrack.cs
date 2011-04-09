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
        public string AlbumName { get; private set; }
        public string AlbumUrl { get; private set; }
        public string AlbumKey { get; private set; }
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
		
		internal RdioTrack() { }
    }
}
