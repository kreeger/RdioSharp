using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

using RdioSharp.Enum;
using RdioSharp.Models;

namespace RdioSharp
{
    public class RdioManager : IRdioManager
    {
        #region Private properties.

        private readonly JavaScriptSerializer _serializer;

        #endregion

        #region Public properties.

        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }
        public string CallBackUrl { get; private set; }
        public string RequestToken { get; private set; }
        public string RequestTokenSecret { get; private set; }
        public string LoginUrl { get; set; }
        public string AccessKey { get; private set; }
        public string AccessKeySecret { get; private set; }
        public string OAuthVerifier { get; private set; }
        public bool IsAuthorized { get { return !string.IsNullOrEmpty(AccessKeySecret); } }

        #endregion

        #region Constants.

        private const string API_URL = "http://api.rdio.com/1/";
        private const string REQUEST_TOKEN_URL = "http://api.rdio.com/oauth/request_token";
        private const string ACCESS_TOKEN_URL = "http://api.rdio.com/oauth/access_token";
        private const string REQUEST_METHOD = "POST";

        #endregion

        #region Constructor.
		
        public RdioManager(string consumerKey = null, string consumerSecret = null,
                           string accessKey = null, string accessSecret = null, string callbackUrl = null)
        {
            CallBackUrl = !string.IsNullOrEmpty(callbackUrl) ? callbackUrl : "oob";
            SetCredentials(consumerKey, consumerSecret, accessKey, accessSecret);
            _serializer = new JavaScriptSerializer();
        }

        #endregion

        #region Manager/OAuth management methods

        /// <summary>
        /// Sets credentials for the Rdio API manager.
        /// </summary>
        /// <param name="consumerKey">The OAuth consumer key.</param>
        /// <param name="consumerSecret">The OAuth consumer secret.</param>
        /// <param name="accessKey">The OAuth access token key.</param>
        /// <param name="accessSecret">The OAuth access token key.</param>
        public void SetCredentials(string consumerKey = null, string consumerSecret = null,
                                   string accessKey = null, string accessSecret = null)
        {
            if (!string.IsNullOrEmpty(consumerKey) && !string.IsNullOrEmpty(consumerSecret))
            {
                ConsumerKey = consumerKey;
                ConsumerSecret = consumerSecret;
            }

            if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(accessSecret)) return;
            AccessKey = accessKey;
            AccessKeySecret = accessSecret;
        }

        /// <summary>
        /// Get the link to Rdio's authorization page for this application.
        /// </summary>
        /// <returns>The url with a valid request token, or a null string.</returns>
        public void GenerateRequestTokenAndLoginUrl()
        {
            var data = new NameValueCollection { { "oauth_callback", CallBackUrl } };

            var response = MakeWebRequest(data, REQUEST_TOKEN_URL);
            if (response.Length <= 0) return;

            //response contains token and token secret.  We only need the token.
            var qs = HttpUtility.ParseQueryString(response);

            if (qs["oauth_callback_confirmed"] != null)
                if (qs["oauth_callback_confirmed"] != "true")
                    throw new Exception("OAuth callback not confirmed.");

            if (qs["oauth_token"] == null) return;

            LoginUrl = string.Format("{0}?oauth_token={1}", qs["login_url"], qs["oauth_token"]);
            RequestToken = qs["oauth_token"];
            RequestTokenSecret = qs["oauth_token_secret"];
        }

        /// <summary>
        /// Exchange the request token for an access token.
        /// </summary>
        /// <param name="oAuthVerifier">An oauth_verifier parameter is provided to the client either in the pre-configured callback URL</param>
        public void Authorize(string oAuthVerifier)
        {
            OAuthVerifier = oAuthVerifier;

            // Make a web request to get the access token; this will already sign it with our verifier
            // which is now no longer null.
            var response = MakeWebRequest(url: ACCESS_TOKEN_URL);

            if (response.Length <= 0) return;

            //Store the Token and Token Secret
            var qs = HttpUtility.ParseQueryString(response);

            if (!string.IsNullOrEmpty(qs["oauth_token"]) && !string.IsNullOrEmpty(qs["oauth_token_secret"]))
                SetCredentials(accessKey: qs["oauth_token"], accessSecret: qs["oauth_token_secret"]);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// This method is refactored to help account for changes between .NET 3.5 and .NET 4.0.
        /// </summary>
        private object Deserialize(string toDeserialize, Type type)
        {
            return _serializer.Deserialize(_serializer, toDeserialize, type, 0);
        }

        #endregion

        #region Rdio API methods

        /// <summary>
        /// <see cref="IRdioManager.AddFriend"/>
        /// </summary>
		public bool AddFriend(string user)
		{
		    var postData = new NameValueCollection
		                       {
		                           {"method", "addFriend"},
		                           {"user", user}
		                       };

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<string>));
            return bool.Parse(((RdioResult<string>)deserialized).Result);
		}

        /// <summary>
        /// <see cref="IRdioManager.AddToCollection"/>
        /// </summary>
        public bool AddToCollection(IEnumerable<string> keys)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "addToCollection"},
                                   {"keys", string.Join(",", keys)}
                               };

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<string>));
            return bool.Parse(((RdioResult<string>)deserialized).Result);
        }

        /// <summary>
        /// <see cref="IRdioManager.AddToPlaylist"/>
        /// </summary>
        public bool AddToPlaylist(string playlist, IEnumerable<string> tracks)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "addToPlaylist"},
                                   {"playlist", playlist},
                                   {"tracks", string.Join(",", tracks)}
                               };

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<string>));
            return bool.Parse(((RdioResult<string>)deserialized).Result);
        }

        /// <summary>
        /// <see cref="IRdioManager.CreatePlaylist"/>
        /// </summary>
        public RdioPlaylist CreatePlaylist(string name, string description, IEnumerable<string> tracks,
                                           IEnumerable<string> extras = null)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "createPlaylist"},
                                   {"name", name},
                                   {"description", description},
                                   {"tracks", string.Join(",", tracks)}
                               };
            if (extras != null && extras.Count() > 0) postData.Add("extras", string.Join(",", extras));

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<RdioPlaylist>));
            return ((RdioResult<RdioPlaylist>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.CurrentUser"/>
        /// </summary>
        public RdioUser CurrentUser(IEnumerable<string> extras = null)
        {
            var postData = new NameValueCollection {{"method", "currentUser"}};
            if (extras != null && extras.Count() > 0) postData.Add("extras", string.Join(",", extras));

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof (RdioResult<RdioUser>));
            return ((RdioResult<RdioUser>) deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.GetActivityStream"/>
        /// </summary>
        public bool DeletePlaylist(string playlist)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "deletePlaylist"},
                                   {"playlist", playlist}
                               };

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<string>));
            return bool.Parse(((RdioResult<string>)deserialized).Result);
        }

        /// <summary>
        /// <see cref="IRdioManager.FindUser"/>
        /// </summary>
        public RdioUser FindUser(string email = null, string vanityName = null)
        {
            var postData = new NameValueCollection {{"method", "findUser"}};
            if (email != null) postData.Add("email", email);
            else if (vanityName != null) postData.Add("vanityName", vanityName);

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<RdioUser>));
            return ((RdioResult<RdioUser>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.Get"/>
        /// </summary>
        public RdioResultSet Get(IEnumerable<string> keys, IEnumerable<string> extras = null)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "get"},
                                   {"keys", string.Join(",", keys)}
                               };
            if (extras != null && extras.Count() > 0) postData.Add("extras", string.Join(",", extras));

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<object>));
            var results = ((RdioResult<object>) deserialized).Result as Dictionary<string, object>;
            return results != null ? RdioFunctions.ConvertDictionaryToRdioResultSet(results) : null;
        }

        /// <summary>
        /// <see cref="IRdioManager.GetActivityStream"/>
        /// </summary>
        public RdioActivityStream GetActivityStream(string user, RdioScope scope = RdioScope.Friends, long lastId = 0)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <see cref="IRdioManager.GetAlbumsForArtist"/>
        /// </summary>
        public IEnumerable<RdioAlbum> GetAlbumsForArtist(string artist, bool featuring = false,
                                                         IEnumerable<string> extras = null, int start = 0,
                                                         int count = 0)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getAlbumsForArtist"},
                                   {"artist", artist}
                               };
            if (featuring) postData.Add("featuring", featuring.ToString());
            if (extras != null && extras.Count() > 0) postData.Add("extras", string.Join(",", extras));
            if (start > 0) postData.Add("start", start.ToString());
            if (count > 0) postData.Add("count", count.ToString());

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<IList<RdioAlbum>>));
            return ((RdioResult<IList<RdioAlbum>>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.GetAlbumsForArtistInCollection"/>
        /// </summary>
        public IEnumerable<RdioAlbum> GetAlbumsForArtistInCollection(string artist, string user = null)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getAlbumsForArtistInCollection"},
                                   {"artist", artist}
                               };
            if (!string.IsNullOrEmpty(user)) postData.Add("user", user);

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<IList<RdioAlbum>>));
            return ((RdioResult<IList<RdioAlbum>>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.GetAlbumsInCollection"/>
        /// </summary>
        public IEnumerable<RdioAlbum> GetAlbumsInCollection(string user = null, int start = 0, int count = 0,
                                                            RdioSortBy sort = RdioSortBy.None, string query = null)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getAlbumsInCollection"}
                               };
            if (!string.IsNullOrEmpty(user)) postData.Add("user", user);
            if (start > 0) postData.Add("start", start.ToString());
            if (count > 0) postData.Add("count", count.ToString());
            if (sort != RdioSortBy.None) postData.Add("sort", sort.ToString());
            if (!string.IsNullOrEmpty(query)) postData.Add("query", query);

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<IList<RdioAlbum>>));
            return ((RdioResult<IList<RdioAlbum>>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.GetHeavyRotation"/>
        /// </summary>
        public IEnumerable<RdioArtist> GetArtistsInCollection(string user = null, int start = 0, int count = 0,
                                                              RdioSortBy sort = RdioSortBy.None, string query = null)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getArtistsInCollection"}
                               };
            if (!string.IsNullOrEmpty(user)) postData.Add("user", user);
            if (start > 0) postData.Add("start", start.ToString());
            if (count > 0) postData.Add("count", count.ToString());
            if (sort != RdioSortBy.None) postData.Add("sort", sort.ToString());
            if (!string.IsNullOrEmpty(query)) postData.Add("query", query);

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<IList<RdioArtist>>));
            return ((RdioResult<IList<RdioArtist>>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.GetHeavyRotation"/>
        /// </summary>
        public IEnumerable<IRdioObject> GetHeavyRotation(string user = null, RdioType type = RdioType.Album,
                                                         bool friends = false, int limit = 0)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getHeavyRotation"},
                                   {"type", type.ToString()}
                               };
            if (!string.IsNullOrEmpty(user)) postData.Add("user", user);
            if (friends) postData.Add("friends", friends.ToString());
            if (limit > 0) postData.Add("limit", limit.ToString());

            var result = MakeWebRequest(postData);
            switch (type)
            {
                case RdioType.Artist:
                    var artists = Deserialize(result, typeof(RdioResult<IList<RdioArtist>>));
                    return ((RdioResult<IList<RdioArtist>>)artists).Result;
                case RdioType.Album:
                    var albums = Deserialize(result, typeof(RdioResult<IList<RdioAlbum>>));
                    return ((RdioResult<IList<RdioAlbum>>)albums).Result;
                default:
                    return null;
            }
        }

        /// <summary>
        /// <see cref="IRdioManager.GetNewReleases"/>
        /// </summary>
        public IEnumerable<RdioAlbum> GetNewReleases(RdioTimeframe timeframe = RdioTimeframe.None, int start = 0,
                                                     int count = 0, IEnumerable<string> extras = null)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getNewReleases"}
                               };
            if (timeframe != RdioTimeframe.None) postData.Add("timeframe", timeframe.ToString());
            if (start > 0) postData.Add("start", start.ToString());
            if (count > 0) postData.Add("count", count.ToString());
            if (extras != null && extras.Count() > 0) postData.Add("extras", string.Join(",", extras));

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<IList<RdioAlbum>>));
            return ((RdioResult<IList<RdioAlbum>>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.GetObjectFromShortCode"/>
        /// </summary>
        public IRdioObject GetObjectFromShortCode(string shortCode)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getObjectFromShortCode"},
                                   {"short_code", shortCode}
                               };

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<object>));
            var results = (Dictionary<string, object>)((RdioResult<object>)deserialized).Result;
            return RdioFunctions.ConvertDictionaryToRdioObject(results);
        }

        /// <summary>
        /// <see cref="IRdioManager.GetObjectFromUrl"/>
        /// </summary>
        public IRdioObject GetObjectFromUrl(string url)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getObjectFromUrl"},
                                   {"url", url}
                               };

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<object>));
            var results = (Dictionary<string, object>)((RdioResult<object>)deserialized).Result;
            return RdioFunctions.ConvertDictionaryToRdioObject(results);
        }

        /// <summary>
        /// <see cref="IRdioManager.GetPlaybackToken"/>
        /// </summary>
        public string GetPlaybackToken(string domain = null)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getPlaybackToken"}
                               };
            if (!string.IsNullOrEmpty(domain)) postData.Add("domain", domain);

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<string>));
            return ((RdioResult<string>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.GetPlaylists"/>
        /// </summary>
        public RdioPlaylistSet GetPlaylists(IEnumerable<string> extras = null)
        {
            var postData = new NameValueCollection { { "method", "getPlaylists" } };
            if (extras != null && extras.Count() > 0) postData.Add("extras", string.Join(",", extras));

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<RdioPlaylistSet>));
            return ((RdioResult<RdioPlaylistSet>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.GetTopCharts"/>
        /// </summary>
        public IEnumerable<IRdioObject> GetTopCharts(RdioType type, int start = 0, int count = 0,
                                                     IEnumerable<string> extras = null)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getTopCharts"},
                                   {"type", type.ToString()}
                               };
            if (start > 0) postData.Add("start", start.ToString());
            if (count > 0) postData.Add("count", count.ToString());
            if (extras != null && extras.Count() > 0) postData.Add("extras", string.Join(",", extras));

            var result = MakeWebRequest(postData);
            switch (type)
            {
                case RdioType.Album:
                    var albums = Deserialize(result, typeof(RdioResult<IList<RdioAlbum>>));
                    return ((RdioResult<IList<RdioAlbum>>)albums).Result;
                case RdioType.Artist:
                    var artists = Deserialize(result, typeof(RdioResult<IList<RdioArtist>>));
                    return ((RdioResult<IList<RdioArtist>>)artists).Result;
                case RdioType.Track:
                    var tracks = Deserialize(result, typeof(RdioResult<IList<RdioTrack>>));
                    return ((RdioResult<IList<RdioTrack>>)tracks).Result;
                case RdioType.Playlist:
                    var playlists = Deserialize(result, typeof(RdioResult<IList<RdioPlaylist>>));
                    return ((RdioResult<IList<RdioPlaylist>>)playlists).Result;
                default:
                    return null;
            }
        }

        /// <summary>
        /// <see cref="IRdioManager.GetTracksForAlbumInCollection"/>
        /// </summary>
        public IEnumerable<RdioTrack> GetTracksForAlbumInCollection(string album, string user = null,
                                                                    IEnumerable<string> extras = null)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getTracksForAlbumInCollection"},
                                   {"album", album}
                               };
            if (!string.IsNullOrEmpty(user)) postData.Add("user", user);
            if (extras != null && extras.Count() > 0) postData.Add("extras", string.Join(",", extras));

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<IList<RdioTrack>>));
            return ((RdioResult<IList<RdioTrack>>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.GetTracksForArtist"/>
        /// </summary>
        public IEnumerable<RdioTrack> GetTracksForArtist(string artist, bool appearsOn = false,
                                                         IEnumerable<string> extras = null, int start = 0, int count = 0)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getTracksForArtist"},
                                   {"artist", artist}
                               };
            if (appearsOn) postData.Add("appearsOn", appearsOn.ToString());
            if (extras != null && extras.Count() > 0) postData.Add("extras", string.Join(",", extras));
            if (start > 0) postData.Add("start", start.ToString());
            if (count > 0) postData.Add("count", count.ToString());

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<IList<RdioTrack>>));
            return ((RdioResult<IList<RdioTrack>>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.GetTracksForArtistInCollection"/>
        /// </summary>
        public IEnumerable<RdioTrack> GetTracksForArtistInCollection(string artist, string user = null,
                                                                     IEnumerable<string> extras = null)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getTracksForArtistInCollection"},
                                   {"artist", artist}
                               };
            if (!string.IsNullOrEmpty(user)) postData.Add("user", user);
            if (extras != null && extras.Count() > 0) postData.Add("extras", string.Join(",", extras));

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<IList<RdioTrack>>));
            return ((RdioResult<IList<RdioTrack>>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.GetTracksInCollection"/>
        /// </summary>
        public IEnumerable<RdioTrack> GetTracksInCollection(string user = null, int start = 0, int count = 0,
                                                            RdioSortBy sort = RdioSortBy.None, string query = null)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "getTracksInCollection"}
                               };
            if (!string.IsNullOrEmpty(user)) postData.Add("user", user);
            if (start > 0) postData.Add("start", start.ToString());
            if (count > 0) postData.Add("count", count.ToString());
            if (sort != RdioSortBy.None) postData.Add("sort", sort.ToString());
            if (!string.IsNullOrEmpty(query)) postData.Add("query", query);

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<IList<RdioTrack>>));
            return ((RdioResult<IList<RdioTrack>>)deserialized).Result;
        }

        /// <summary>
        /// <see cref="IRdioManager.RemoveFriend"/>
        /// </summary>
        public bool RemoveFriend(string user)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "removeFriend"},
                                   {"user", user}
                               };

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<string>));
            return bool.Parse(((RdioResult<string>)deserialized).Result);
        }

        /// <summary>
        /// <see cref="IRdioManager.RemoveFromCollection"/>
        /// </summary>
        public bool RemoveFromCollection(IEnumerable<string> keys)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "removeFromCollection"},
                                   {"keys", string.Join(",", keys)}
                               };

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<string>));
            return bool.Parse(((RdioResult<string>)deserialized).Result);
        }

        /// <summary>
        /// <see cref="IRdioManager.RemoveFromPlaylist"/>
        /// </summary>
        public bool RemoveFromPlaylist(string playlist, IEnumerable<string> tracks, int index = 0, int count = 0)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "removeFromPlaylist"},
                                   {"playlist", playlist},
                                   {"tracks", string.Join(",", tracks)},
                                   {"index", index.ToString()},
                                   {"count", count <= 0 ? tracks.Count().ToString() : count.ToString()}
                               };

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<string>));
            return bool.Parse(((RdioResult<string>)deserialized).Result);
        }

        /// <summary>
        /// <see cref="IRdioManager.Search"/>
        /// </summary>
        public RdioResultSet Search(string query, IEnumerable<RdioType> types, bool neverOr = true,
                                       IEnumerable<string> extras = null, int start = 0, int count = 0)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "search"},
                                   {"query", query},
                                   {"types", string.Join(",", types)}
                               };
            if (!neverOr) postData.Add("never_or", neverOr.ToString().ToLower());
            if (extras != null && extras.Count() > 0) postData.Add("extras", string.Join(",", extras));
            if (start > 0) postData.Add("start", start.ToString());
            if (count > 0) postData.Add("count", count.ToString());

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<object>));
            var results = (Dictionary<string, object>)((RdioResult<object>)deserialized).Result;
            var searchResults = new List<object>((object[])results["results"]);
            return RdioFunctions.ConvertListToRdioResultSet(searchResults);
        }

        /// <summary>
        /// <see cref="IRdioManager.SearchSuggestions"/>
        /// </summary>
        public RdioResultSet SearchSuggestions(string query, IEnumerable<string> extras = null)
        {
            var postData = new NameValueCollection
                               {
                                   {"method", "searchSuggestions"},
                                   {"query", query}
                               };
            if (extras != null && extras.Count() > 0) postData.Add("extras", string.Join(",", extras));

            var result = MakeWebRequest(postData);
            var deserialized = Deserialize(result, typeof(RdioResult<object>));
            var results = new List<object>((object[])((RdioResult<object>)deserialized).Result);
            return RdioFunctions.ConvertListToRdioResultSet(results);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Submit a web request using oAuth.
        /// </summary>
        /// <param name="url">The full url, including the querystring.</param>
        /// <param name="postData">Data to post, as a <see cref="NameValueCollection"/>.</param>
        /// <returns>The web server response.</returns>
        private string MakeWebRequest(NameValueCollection postData = null, string url = API_URL)
        {
            string outUrl;
            string querystring;
            string postString;

            //Setup postData for signing.
            //Add the postData to the querystring.
            if (postData != null && postData.Count > 0)
            {
                //Decode the parameters and re-encode using the oAuth UrlEncode method.
                postString = string.Empty;
                foreach (var key in postData.AllKeys)
                {
                    if (postString.Length > 0) postString += "&";
                    // Decode as key.
                    postData[key] = HttpUtility.UrlDecode(postData[key]);
                    // Re-encode as key.
                    postData[key] = OAuth.UrlEncode(postData[key]);
                    // Append to outstring.
                    postString += key + "=" + postData[key];

                }
                if (url.IndexOf("?") > 0) url += "&";
                else url += "?";
                url += postString;
            }

            var uri = new Uri(url);

            var nonce = OAuth.GenerateNonce();
            var timeStamp = OAuth.GenerateTimeStamp();

            // Generate Signature
            // If we have an access key...
            var sig = OAuth.GenerateSignature(uri, ConsumerKey, ConsumerSecret, AccessKey ?? RequestToken,
                                              AccessKeySecret ?? RequestTokenSecret, CallBackUrl, OAuthVerifier,
                                              REQUEST_METHOD, timeStamp, nonce, out outUrl, out querystring);

            querystring += "&oauth_signature=" + OAuth.UrlEncode(sig);

            // Convert the querystring to postData
            postString = querystring;
            querystring = "";

            if (querystring.Length > 0) outUrl += "?";

            return DoWebRequest(outUrl + querystring, postString);
        }

        /// <summary>
        /// Web Request Wrapper
        /// </summary>
        /// <param name="url">Full url to the web resource</param>
        /// <param name="postData">Data to post in querystring format</param>
        /// <returns>The web server response.</returns>
        private string DoWebRequest(string url, string postData)
        {
            string responseData = null;
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                // Set request details.
                webRequest.Method = REQUEST_METHOD;
                webRequest.ServicePoint.Expect100Continue = false;
                webRequest.ContentType = "application/x-www-form-urlencoded";

                // For getting through any proxy
                webRequest.Proxy = WebRequest.DefaultWebProxy;
                webRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;

                // Write the post data into the request's stream.
                var requestWriter = new StreamWriter(webRequest.GetRequestStream());
                try
                {
                    requestWriter.Write(postData);
                }
                finally
                {
                    requestWriter.Close();
                }

                // Process the data.
                StreamReader responseReader = null;
                Stream responseStream = null;

                try
                {
                    responseStream = webRequest.GetResponse().GetResponseStream();
                    if (responseStream != null)
                    {
                        responseReader = new StreamReader(responseStream);
                        responseData = responseReader.ReadToEnd();
                    }
                }
                finally
                {
                    if (responseStream != null) responseStream.Close();
                    if (responseReader != null) responseReader.Close();
                }
            }

            return responseData;
        }

        #endregion
    }
}
