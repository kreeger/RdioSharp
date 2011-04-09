using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioSearchResult
    {
        public int ResultCount { get; set; }
        public int AlbumCount { get; set; }
        public int ArtistCount { get; set; }
        public int PersonCount { get; set; }
        public int PlaylistCount { get; set; }
        public int TrackCount { get; set; }
        public IList<IRdioObject> Results { get; set; }
		
		internal RdioSearchResult() { }
		
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
