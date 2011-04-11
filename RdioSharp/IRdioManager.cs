using System.Collections.Generic;

using RdioSharp.Enum;
using RdioSharp.Models;

namespace RdioSharp
{
    public interface IRdioManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool AddFriend(string user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        bool AddToCollection(IEnumerable<string> keys);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playlist"></param>
        /// <param name="tracks"></param>
        /// <returns></returns>
        bool AddToPlaylist(string playlist, IEnumerable<string> tracks);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="tracks"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        RdioPlaylist CreatePlaylist(string name, string description, IEnumerable<string> tracks,
                                    IEnumerable<string> extras = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extras"></param>
        /// <returns></returns>
        RdioUser CurrentUser(IEnumerable<string> extras = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        bool DeletePlaylist(string playlist);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="vanityName"></param>
        /// <returns></returns>
        RdioUser FindUser(string email = null, string vanityName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        IEnumerable<IRdioObject> Get(IEnumerable<string> keys, IEnumerable<string> extras = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="scope"></param>
        /// <param name="lastId"></param>
        /// <returns></returns>
        RdioActivityStream GetActivityStream(string user, RdioScope scope = RdioScope.Friends, long lastId = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="artist"></param>
        /// <param name="featuring"></param>
        /// <param name="extras"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<RdioAlbum> GetAlbumsForArtist(string artist, bool featuring = false,
                                                  IEnumerable<string> extras = null,
                                                  int start = 0, int count = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="artist"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<RdioAlbum> GetAlbumsForArtistInCollection(string artist, string user = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="sort"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<RdioAlbum> GetAlbumsInCollection(string user = null, int start = 0, int count = 0,
                                                     RdioSortBy sort = RdioSortBy.None, string query = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="sort"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<RdioArtist> GetArtistsInCollection(string user = null, int start = 0, int count = 0,
                                                       RdioSortBy sort = RdioSortBy.None, string query = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="type"></param>
        /// <param name="friends"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IEnumerable<IRdioObject> GetHeavyRotation(string user, RdioHeavyRotationType type = RdioHeavyRotationType.Albums,
                                                  bool friends = false, int limit = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeframe"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        IEnumerable<RdioAlbum> GetNewReleases(RdioTimeframe timeframe = RdioTimeframe.None, int start = 0, int count = 0,
                                              IEnumerable<string> extras = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortCode"></param>
        /// <returns></returns>
        IRdioObject GetObjectFromShortCode(string shortCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        IRdioObject GetObjectFromUrl(string url);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        string GetPlaybackToken(string domain = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extras"></param>
        /// <returns></returns>
        RdioPlaylistSet GetPlaylists(IEnumerable<string> extras = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        IEnumerable<IRdioObject> GetTopCharts(RdioType type, int start = 0, int count = 0,
                                              IEnumerable<string> extras = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="album"></param>
        /// <param name="user"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        IEnumerable<RdioTrack> GetTracksForAlbumInCollection(string album, string user = null,
                                                             IEnumerable<string> extras = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="artist"></param>
        /// <param name="appearsOn"></param>
        /// <param name="extras"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<RdioTrack> GetTracksForArtist(string artist, bool appearsOn = false,
                                                  IEnumerable<string> extras = null, int start = 0, int count = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="artist"></param>
        /// <param name="user"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        IEnumerable<RdioTrack> GetTracksForArtistInCollection(string artist, string user = null,
                                                              IEnumerable<string> extras = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="sort"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<RdioTrack> GetTracksInCollection(string user = null, int start = 0, int count = 0,
                                                     RdioSortBy sort = RdioSortBy.None, string query = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool RemoveFriend(string user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        bool RemoveFromCollection(IEnumerable<string> keys);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playlist"></param>
        /// <param name="tracks"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        bool RemoveFromPlaylist(string playlist, IEnumerable<string> tracks, int index = 0, int count = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="types"></param>
        /// <param name="neverOr"></param>
        /// <param name="extras"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        RdioSearchResult Search(string query, IList<RdioType> types, bool neverOr = true,
                                IList<string> extras = null, int start = 0, int count = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        IEnumerable<IRdioObject> SearchSuggestions(string query, IEnumerable<string> extras = null);
    }
}
