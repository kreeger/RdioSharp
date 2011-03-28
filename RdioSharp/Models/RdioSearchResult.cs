using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioSearchResult
    {
        public int ResultCount { get; private set; }
        public int AlbumCount { get; private set; }
        public int ArtistCount { get; private set; }
        public int PersonCount { get; private set; }
        public int PlaylistCount { get; private set; }
        public int TrackCount { get; private set; }
        public IList<IRdioObject> Results { get; private set; }

        internal RdioSearchResult(int resultCount, int albumCount, int artistCount, int personCount,
                                  int playlistCount, int trackCount, IList<IRdioObject> results)
        {
            ResultCount = resultCount;
            AlbumCount = albumCount;
            ArtistCount = artistCount;
            PersonCount = personCount;
            PlaylistCount = playlistCount;
            TrackCount = trackCount;
            Results = results;
        }
    }
}
