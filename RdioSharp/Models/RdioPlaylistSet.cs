using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioPlaylistSet
    {
        public IList<RdioPlaylist> OwnedPlaylists { get; set; }
        public IList<RdioPlaylist> CollaboratedPlaylists { get; set; }
        public IList<RdioPlaylist> SubscribedPlaylists { get; set; }

        internal RdioPlaylistSet() { }
    }
}
