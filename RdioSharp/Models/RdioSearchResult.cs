using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioSearchResult
    {
        public int ResultCount { get; set; }
        public IList<RdioAlbum> Albums { get; set; }
        public IList<RdioArtist> Artists { get; set; }
        public IList<RdioUser> Users { get; set; }
        public IList<RdioPlaylist> Playlists { get; set; }
        public IList<RdioTrack> Tracks { get; set; }
		
		internal RdioSearchResult()
		{
		    Albums = new List<RdioAlbum>();
            Artists = new List<RdioArtist>();
            Users = new List<RdioUser>();
            Playlists = new List<RdioPlaylist>();
            Tracks = new List<RdioTrack>();
		}
    }
}
