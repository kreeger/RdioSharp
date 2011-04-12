using System.Collections.Generic;

using RdioSharp.Enum;
using RdioSharp.Models;

namespace RdioSharp
{
    public interface IRdioManager
    {
        /// <summary>
        /// Add a friend to the current user. Requires authentication.
        /// </summary>
        /// <param name="user">Required. The key of the user to add.</param>
        /// <returns>True if the add succeeds, false if it fails.</returns>
        bool AddFriend(string user);

        /// <summary>
        /// Add tracks or playlists to the current user's collection. Requires authentication.
        /// </summary>
        /// <param name="keys">Required. The list of tracks and playlist to add to the collection.</param>
        /// <returns>True.</returns>
        bool AddToCollection(IEnumerable<string> keys);

        /// <summary>
        /// Add a track to a playlist. Requires authentication.
        /// </summary>
        /// <param name="playlist">Required. The key of the playlist to add to.</param>
        /// <param name="tracks">Required. The keys of the tracks to add to the playlist.</param>
        /// <returns>True.</returns>
        bool AddToPlaylist(string playlist, IEnumerable<string> tracks);

        /// <summary>
        /// Create a new playlist in the current user's collection. Requires authentication.
        /// </summary>
        /// <param name="name">Required. The playlist name.</param>
        /// <param name="description">Required. The playlist description.</param>
        /// <param name="tracks">Required. The keys of the initial tracks to start the playlist off.</param>
        /// <param name="extras">Optional. A list of additional fields to return.</param>
        /// <returns>The new <see cref="RdioPlaylist"/>.</returns>
        RdioPlaylist CreatePlaylist(string name, string description, IEnumerable<string> tracks,
                                    IEnumerable<string> extras = null);

        /// <summary>
        /// Get information about the currently logged in user. Requires authentication.
        /// </summary>
        /// <param name="extras">Optional. A list of additional fields to return.</param>
        /// <returns>The currently logged in <see cref="RdioUser"/>.</returns>
        RdioUser CurrentUser(IEnumerable<string> extras = null);

        /// <summary>
        /// Delete a playlist. Requires authentication.
        /// </summary>
        /// <param name="playlist">Required. The key of the playlist to delete.</param>
        /// <returns>True on success, false on failure.</returns>
        bool DeletePlaylist(string playlist);

        /// <summary>
        /// Find a user either by email address or by their username. Exactly one of email or
        /// vanityName must be supplied. Does not require authentication.
        /// </summary>
        /// <param name="email">An email address.</param>
        /// <param name="vanityName">A username.</param>
        /// <returns>A matching <see cref="RdioUser"/>, or null if none is found.</returns>
        RdioUser FindUser(string email = null, string vanityName = null);

        /// <summary>
        /// Fetch one or more objects from Rdio. Does not require authentication.
        /// </summary>
        /// <param name="keys">Required. A list of keys for the objects to fetch.</param>
        /// <param name="extras">Optional. A list of additional fields to return.</param>
        /// <returns>A list of <see cref="IRdioObject"/>s where matches were found.</returns>
        IEnumerable<IRdioObject> Get(IEnumerable<string> keys, IEnumerable<string> extras = null);

        /// <summary>
        /// Get the activity events for a user, a user's friends, or everyone on Rdio. Does not require
        /// authentication.
        /// </summary>
        /// <param name="user">Required. The key of the user to retrieve an activity stream for.</param>
        /// <param name="scope">Required. The scope of the activity stream. Defaults to Friends if not
        /// supplied.</param>
        /// <param name="lastId">Optional. The last_id returned by the last call to this method. If supplied,
        /// only activity since that call will be returned.</param>
        /// <returns>An <see cref="RdioActivityStream"/> object with detailed records of each event.</returns>
        RdioActivityStream GetActivityStream(string user, RdioScope scope = RdioScope.Friends, long lastId = 0);

        /// <summary>
        /// Return the albums by (or featuring) an artist. Does not require authentication.
        /// </summary>
        /// <param name="artist">Required. The key of the artist.</param>
        /// <param name="featuring">Optional. True returns albums featuring the artist, rather than albums
        /// credited to the artist.</param>
        /// <param name="extras">Optional. A list of additional fields to return.</param>
        /// <param name="start">Optional. The offset of the first result to return.</param>
        /// <param name="count">Optional. The maxiumum number of results to return.</param>
        /// <returns>A list of <see cref="RdioAlbum"/>s.</returns>
        IEnumerable<RdioAlbum> GetAlbumsForArtist(string artist, bool featuring = false,
                                                  IEnumerable<string> extras = null,
                                                  int start = 0, int count = 0);

        /// <summary>
        /// Get the albums in the user's collection by a particular artist. Does not require authentication.
        /// </summary>
        /// <param name="artist">Required. The key of the artist.</param>
        /// <param name="user">Optional. The key of the collection user.</param>
        /// <returns>A list of <see cref="RdioAlbum"/>s.</returns>
        IEnumerable<RdioAlbum> GetAlbumsForArtistInCollection(string artist, string user = null);

        /// <summary>
        /// Get all of the albums in the user's collection. Does not require authentication.
        /// </summary>
        /// <param name="user">Optional. The collection user.</param>
        /// <param name="start">Optional. The offset of the first result to return.</param>
        /// <param name="count">Optional. The maxiumum number of results to return.</param>
        /// <param name="sort">Optional. What to sort by. Valid values are DateAdded, PlayCount,
        /// Artist, and Name.</param>
        /// <param name="query">Optional. The search prefix.</param>
        /// <returns>A list of <see cref="RdioAlbum"/>s.</returns>
        IEnumerable<RdioAlbum> GetAlbumsInCollection(string user = null, int start = 0, int count = 0,
                                                     RdioSortBy sort = RdioSortBy.None, string query = null);

        /// <summary>
        /// Get all of the artist in a user's collection. Does not require authentication.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="start">Optional. The offset of the first result to return.</param>
        /// <param name="count">Optional. The maxiumum number of results to return.</param>
        /// <param name="sort">Optional. What to sort by. Only Name is supported.</param>
        /// <param name="query">Optional. The search prefix.</param>
        /// <returns>A list of <see cref="RdioArtist"/>s.</returns>
        IEnumerable<RdioArtist> GetArtistsInCollection(string user = null, int start = 0, int count = 0,
                                                       RdioSortBy sort = RdioSortBy.None, string query = null);

        /// <summary>
        /// Find the most popular artists or albums for a user, their friends, or the whole site. Does not
        /// require authentication. Does not require authentication.
        /// </summary>
        /// <param name="user">Optional. The key of the user. If this is not supplied, heavy rotation for
        /// everyone is returned.</param>
        /// <param name="type">Optional. Either Artist or Album; default is Album.</param>
        /// <param name="friends">Optional. If true, returns the user's friends heavy rotation instead.</param>
        /// <param name="limit">Optional. The maximum number of results to return.</param>
        /// <returns>A list of <see cref="IRdioObject"/>s, either <see cref="RdioAlbum"/>s or <see cref="RdioArtist"/>s,
        /// based on the <see cref="RdioType"/> specified.</returns>
        IEnumerable<IRdioObject> GetHeavyRotation(string user = null, RdioType type = RdioType.Album,
                                                  bool friends = false, int limit = 0);

        /// <summary>
        /// Return new albums released across a timeframe. Does not require authentication.
        /// </summary>
        /// <param name="timeframe"></param>
        /// <param name="start">Optional. The offset of the first result to return.</param>
        /// <param name="count">Optional. The maxiumum number of results to return.</param>
        /// <param name="extras">Optional. A list of additional fields to return.</param>
        /// <returns>A list of <see cref="RdioAlbum"/>s.</returns>
        IEnumerable<RdioAlbum> GetNewReleases(RdioTimeframe timeframe = RdioTimeframe.None, int start = 0, int count = 0,
                                              IEnumerable<string> extras = null);

        /// <summary>
        /// Return the object that the supplied Rdio short-code is a representation of. Requires authentication.
        /// </summary>
        /// <param name="shortCode">Required. The short code (everything after http://rd.io/x/).</param>
        /// <returns>The <see cref="IRdioObject"/> the short code links to.</returns>
        IRdioObject GetObjectFromShortCode(string shortCode);

        /// <summary>
        /// Return the object that the supplied Rdio url is a representation of. Requires authentication.
        /// </summary>
        /// <param name="url">Required. The path portion of the url.</param>
        /// <returns>The <see cref="IRdioObject"/> represented at the url.</returns>
        IRdioObject GetObjectFromUrl(string url);

        /// <summary>
        /// Get an playback token. If you are using this for web playback you must supply a domain. Does not require
        /// authentication.
        /// </summary>
        /// <param name="domain">Optional. The domain that the playback SWF will be embedded in. Required if
        /// you are using this for web playback.</param>
        /// <returns>A playback token string.</returns>
        string GetPlaybackToken(string domain = null);

        /// <summary>
        /// Get the current user's playlists. Requires authentication.
        /// </summary>
        /// <param name="extras">Optional. A list of additional fields to return.</param>
        /// <returns>A <see cref="RdioPlaylistSet"/> with lists for each set of playlists: owned, subscribed,
        /// and collaborations.</returns>
        RdioPlaylistSet GetPlaylists(IEnumerable<string> extras = null);

        /// <summary>
        /// Return the site-wide most popular items for a given type. Does not require authentication.
        /// </summary>
        /// <param name="type">Required. The types to include in the results. Valid values are Artist,
        /// Album, Track, or Playlist.</param>
        /// <param name="start">Optional. The offset of the first result to return.</param>
        /// <param name="count">Optional. The maxiumum number of results to return.</param>
        /// <param name="extras">Optional. A list of additional fields to return.</param>
        /// <returns>A list of <see cref="IRdioObject"/>s depending on the <see cref="RdioType"/></returns>
        IEnumerable<IRdioObject> GetTopCharts(RdioType type, int start = 0, int count = 0,
                                              IEnumerable<string> extras = null);

        /// <summary>
        /// Which tracks on the given album are in the user's collection. Does not require authentication.
        /// </summary>
        /// <param name="album">Required. The key of the album.</param>
        /// <param name="user">Optional. The key of the collection user.</param>
        /// <param name="extras">Optional. A list of additional fields to return.</param>
        /// <returns>A list of <see cref="RdioTrack"/>s.</returns>
        IEnumerable<RdioTrack> GetTracksForAlbumInCollection(string album, string user = null,
                                                             IEnumerable<string> extras = null);

        /// <summary>
        /// Get all of the tracks by this artist. Does not require authentication.
        /// </summary>
        /// <param name="artist"></param>
        /// <param name="appearsOn"></param>
        /// <param name="extras">Optional. A list of additional fields to return.</param>
        /// <param name="start">Optional. The offset of the first result to return.</param>
        /// <param name="count">Optional. The maxiumum number of results to return.</param>
        /// <returns>A list of <see cref="RdioTrack"/>s.</returns>
        IEnumerable<RdioTrack> GetTracksForArtist(string artist, bool appearsOn = false,
                                                  IEnumerable<string> extras = null, int start = 0, int count = 0);

        /// <summary>
        /// Which tracks from the given artist are in the user's collection. Does not require authentication.
        /// </summary>
        /// <param name="artist">Required. The key of the artist.</param>
        /// <param name="user">Optional. The key of the user whose collection to examine.</param>
        /// <param name="extras">Optional. A list of additional fields to return.</param>
        /// <returns>A list of <see cref="RdioTrack"/>s.</returns>
        IEnumerable<RdioTrack> GetTracksForArtistInCollection(string artist, string user = null,
                                                              IEnumerable<string> extras = null);

        /// <summary>
        /// Get all of the tracks in the user's collection. Does not require authentication.
        /// </summary>
        /// <param name="user">Optional. The key of the collection user.</param>
        /// <param name="start">Optional. The offset of the first result to return.</param>
        /// <param name="count">Optional. The maxiumum number of results to return.</param>
        /// <param name="sort">Optional. Sory by; valid values are DateAdded, PlayCount, Artist,
        /// Album, and Name.</param>
        /// <param name="query">Optional. The search prefix.</param>
        /// <returns>A list of <see cref="RdioTrack"/>s.</returns>
        IEnumerable<RdioTrack> GetTracksInCollection(string user = null, int start = 0, int count = 0,
                                                     RdioSortBy sort = RdioSortBy.None, string query = null);

        /// <summary>
        /// Remove a friend from the current user. Requires authentication.
        /// </summary>
        /// <param name="user">Required. The user to remove.</param>
        /// <returns>True if success, false if failure.</returns>
        bool RemoveFriend(string user);

        /// <summary>
        /// Remove tracks or playlists from the current user's collection. Requires authentication.
        /// </summary>
        /// <param name="keys">Required. The list of track or playlist keys to remove from the collection.</param>
        /// <returns>True if success, false if failure.</returns>
        bool RemoveFromCollection(IEnumerable<string> keys);

        /// <summary>
        /// Remove an item from a playlist by its position in the playlist. Requires authentication.
        /// </summary>
        /// <param name="playlist">Required. The playlist to modify.</param>
        /// <param name="tracks">Required. The keys of the tracks to remove.</param>
        /// <param name="index">Optional. The index of the first item to remove. If not supplied, the
        /// default is 0.</param>
        /// <param name="count">Optional. The maxiumum number of results to return. If not supplied, the count
        /// of tracks is used.</param>
        /// <returns>True if success, false if failure.</returns>
        bool RemoveFromPlaylist(string playlist, IEnumerable<string> tracks, int index = 0, int count = 0);

        /// <summary>
        /// Search for artists, albums, tracks, users or all kinds of objects. Does not require authentication.
        /// </summary>
        /// <param name="query">Required. The search prefix.</param>
        /// <param name="types">Required. Types to include in results. Valid values are
        /// Artist, Album, Track, Playlist, and User.</param>
        /// <param name="neverOr">Optional. By default, search uses an AND query that falls back to an
        /// OR query if there are no results. Passing false here will disable that fallback.</param>
        /// <param name="extras">Optional. A list of additional fields to return.</param>
        /// <param name="start">Optional. The offset of the first result to return.</param>
        /// <param name="count">Optional. The maxiumum number of results to return.</param>
        /// <returns>A <see cref="RdioSearchResult"/> containing a list for each type of result found.</returns>
        RdioSearchResult Search(string query, IList<RdioType> types, bool neverOr = true,
                                IList<string> extras = null, int start = 0, int count = 0);

        /// <summary>
        /// Match the supplied prefix against artists, albums, tracks and people in the Rdio system. Does not
        /// require authentication.
        /// </summary>
        /// <param name="query">Required. The search prefix.</param>
        /// <param name="extras">Optional. A list of additional fields to return.</param>
        /// <returns>A list of <see cref="IRdioObject"/>s matching the search prefix.</returns>
        IEnumerable<IRdioObject> SearchSuggestions(string query, IEnumerable<string> extras = null);
    }
}
