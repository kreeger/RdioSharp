using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioResultSet : IRdioBaseObject
    {
        public IList<RdioAlbum> Albums { get; set; }
        public IList<RdioArtist> Artists { get; set; }
        public IList<RdioPlaylist> Playlists { get; set; }
        public IList<RdioTrack> Tracks { get; set; }
        public IList<RdioUser> Users { get; set; }

        public RdioResultSet()
        {
            Albums = new List<RdioAlbum>();
            Artists = new List<RdioArtist>();
            Playlists = new List<RdioPlaylist>();
            Tracks = new List<RdioTrack>();
            Users = new List<RdioUser>();
        }
    }
}
