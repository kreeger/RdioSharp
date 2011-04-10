using System;
using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioTrack : IRdioMusicObject
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string BaseIcon { get; set; }
        public RdioType RdioType { get; set; }
        public string ArtistName { get; set; }
        public string ArtistUrl { get; set; }
        public string ArtistKey { get; set; }
        public string AlbumName { get; set; }
        public string AlbumUrl { get; set; }
        public string AlbumKey { get; set; }
        public bool IsExplicit { get; set; }
        public bool IsClean { get; set; }
        public decimal Price { get; set; }
        public bool CanStream { get; set; }
        public bool CanSample { get; set; }
        public bool CanTether { get; set; }
        public string ShortUrl { get; set; }
        public string EmbedUrl { get; set; }
        public TimeSpan Duration { get; set; }
        public string AlbumArtistName { get; set; }
        public string AlbumArtistKey { get; set; }
        public bool CanDownload { get; set; }
        public bool CanDownloadAlbumOnly { get; set; }
        public int PlayCount { get; set; }
		
		internal RdioTrack() { }
    }
}
