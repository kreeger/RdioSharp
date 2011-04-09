using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RdioSharp.Models
{
    public class RdioAlbum : IRdioMusicObject
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string BaseIcon { get; set; }
        public string ArtistName { get; set; }
        public string ArtistUrl { get; set; }
        public string ArtistKey { get; set; }
        public bool IsExplicit { get; set; }
        public bool IsClean { get; set; }
        public decimal Price { get; set; }
        public bool CanStream { get; set; }
        public bool CanSample { get; set; }
        public bool CanTether { get; set; }
        public string ShortUrl { get; set; }
        public string EmbedUrl { get; set; }
        public TimeSpan Duration { get; set; }
        public RdioType RdioType { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IList<string> TrackKeys { get; set; }

        internal RdioAlbum() { }
    }
}
