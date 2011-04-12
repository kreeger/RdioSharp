using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json.Linq;

using RdioSharp.Enum;
using RdioSharp.Models;

namespace RdioSharp
{
	public static class RdioFunctions
    {
        #region Private internally-used stuff

        private static RdioType ParseRdioType(string input)
        {
            switch (input)
            {
                case "r":
                case "ar": return RdioType.Artist;
                case "a":
                case "al": return RdioType.Album;
                case "t": return RdioType.Track;
                case "p": return RdioType.Playlist;
                case "s": return RdioType.User;
                default: return RdioType.Unknown;
            }
        }

	    private static JObject CheckStatusAndGetResult(string input)
		{
			var parsed = JObject.Parse(input);
			var status = (string)parsed["status"];
			if (status != null)
			{
				if (status != "ok") return null;
				parsed = (JObject)parsed["result"];
			}

	        return parsed;
		}

        private static bool CheckStatusAndGetBoolean(string input)
        {
            var parsed = JObject.Parse(input);
            var status = (string)parsed["status"];
            var result = false;
            if (status != null && status == "ok")
                result = (bool)parsed["result"];

            return result;
        }

        private static DateTime ParseStringToDateTime(string input)
        {
            var dateList = input.Split('-');
            return new DateTime(int.Parse(dateList[0]), int.Parse(dateList[1]), int.Parse(dateList[2]));
        }

        #endregion

        #region Public stuff

        public static bool ParseJSONToBooleanResult(string input)
        {
            return CheckStatusAndGetBoolean(input);
        }

        public static IEnumerable<IRdioObject> ParseJSONListStringToRdioObjectList(string input)
        {
			var list = new List<IRdioObject>();
            var parsed = CheckStatusAndGetResult(input);
            foreach (var kvp in parsed)
            {
                list.Add(ParseJSONStringToRdioObject(kvp.Value.ToString()));
            }

            return list;
        }
		
		public static RdioSearchResult ParseJSONStringToRdioSearchResult(string input)
		{
            var parsed = CheckStatusAndGetResult(input);
			
			var searchResult = new RdioSearchResult {
				ResultCount = (int)parsed["number_results"]
			};

            foreach (var parsedResult in
                parsed["results"].Select(result => ParseJSONStringToRdioObject(result.ToString())))
            {
                switch (parsedResult.RdioType)
                {
                    case RdioType.Album:
                        searchResult.Albums.Add((RdioAlbum)parsedResult);
                        break;
                    case RdioType.Artist:
                        searchResult.Artists.Add((RdioArtist)parsedResult);
                        break;
                    case RdioType.Playlist:
                        searchResult.Playlists.Add((RdioPlaylist)parsedResult);
                        break;
                    case RdioType.Track:
                        searchResult.Tracks.Add((RdioTrack)parsedResult);
                        break;
                    case RdioType.User:
                        searchResult.Users.Add((RdioUser)parsedResult);
                        break;
                    default: break;
                }
            }

		    return searchResult;

		}

        public static RdioActivityStream ParseJSONStringToRdioActivityStream(string input)
        {
            var parsed = CheckStatusAndGetResult(input);

            return null;
        }

        public static IRdioObject ParseJSONStringToRdioObject(string input)
        {
            IRdioObject rdioObject = null;
            var parsed = CheckStatusAndGetResult(input);

            var rdioType = ParseRdioType((string)parsed["type"]);
            var result = new Dictionary<string, object>
                             {
                                 { "key", (string) parsed["key"] },
                                 { "url", (string)parsed["url"] },
                                 { "icon", (string)parsed["icon"] },
                                 { "baseIcon", (string)parsed["baseIcon"] },
                                 { "rdioType", rdioType }
                             };

            switch (rdioType)
            {
                case RdioType.Album:
                    JToken trackKeys;
                    rdioObject = new RdioAlbum
                                     {
                                         Name = (string) parsed["name"],
                                         ArtistName = (string) parsed["artist"],
                                         ArtistUrl = (string) parsed["artistUrl"],
                                         ArtistKey = (string) parsed["artistKey"],
                                         IsExplicit = (bool) parsed["isExplicit"],
                                         IsClean = (bool) parsed["isClean"],
                                         Price = (string) parsed["price"] == "None" ? 0 :
                                            Decimal.Parse((string) parsed["price"]),
                                         CanStream = (bool) parsed["canStream"],
                                         CanSample = (bool) parsed["canSample"],
                                         CanTether = (bool) parsed["canTether"],
                                         ShortUrl = (string) parsed["shortUrl"],
                                         EmbedUrl = (string) parsed["embedUrl"],
                                         Duration = new TimeSpan(0, 0, (int) parsed["duration"]),
                                         ReleaseDate = ParseStringToDateTime((string) parsed["releaseDate"])
                                     };
                    if (parsed.TryGetValue("trackKeys", out trackKeys))
                        ((RdioAlbum)rdioObject).TrackKeys = trackKeys.Select(item => (string)item).ToList();
                    break;
                case RdioType.Artist:
                    JToken albumCount;
                    rdioObject = new RdioArtist
                                     {
                                         Name = (string) parsed["name"],
                                         TrackCount = (int) parsed["length"],
                                         HasRadio = (bool) parsed["hasRadio"],
                                         ShortUrl = (string) parsed["shortUrl"]
                                     };
                    if (parsed.TryGetValue("albumCount", out albumCount))
                        ((RdioArtist)rdioObject).AlbumCount = (int) albumCount;
                    break;
                case RdioType.Playlist:
                    result.Add("name", (string)parsed["name"]);
                    result.Add("length", (int)parsed["length"]);
                    result.Add("owner", (string)parsed["owner"]);
                    result.Add("ownerUrl", (string)parsed["ownerUrl"]);
                    result.Add("ownerKey", (string)parsed["ownerKey"]);
                    result.Add("ownerIcon", (string)parsed["ownerIcon"]);
                    result.Add("lastUpdated", DateTime.Parse((string)parsed["lastUpdated"]));
                    result.Add("shortUrl", (string)parsed["shortUrl"]);
                    result.Add("embedUrl", (string)parsed["embedUrl"]);
                    rdioObject = new RdioPlaylist
                                     {
                                         Name = (string) parsed["name"],
                                         TrackCount = (int) parsed["length"],
                                         OwnerName = (string) parsed["owner"],
                                         OwnerUrl = (string) parsed["ownerUrl"],
                                         OwnerKey = (string) parsed["ownerKey"],
                                         OwnerIcon = (string) parsed["ownerIcon"],
                                         LastUpdated = DateTime.Parse((string) parsed["lastUpdated"]),
                                         ShortUrl = (string) parsed["shortUrl"],
                                         EmbedUrl = (string) parsed["embedUrl"]
                                     };
                    break;
                case RdioType.Track:
                    JToken playCount;
                    rdioObject = new RdioTrack
                                     {
                                         Name = (string) parsed["name"],
                                         ArtistName = (string) parsed["artist"],
                                         ArtistUrl = (string) parsed["artistUrl"],
                                         ArtistKey = (string) parsed["artistKey"],
                                         AlbumName = (string) parsed["album"],
                                         AlbumUrl = (string) parsed["albumUrl"],
                                         AlbumKey = (string) parsed["albumKey"],
                                         IsExplicit = (bool) parsed["isExplicit"],
                                         IsClean = (bool) parsed["isClean"],
                                         Price = Decimal.Parse((string) parsed["price"]),
                                         CanStream = (bool) parsed["canStream"],
                                         CanSample = (bool) parsed["canSample"],
                                         CanTether = (bool) parsed["canTether"],
                                         ShortUrl = (string) parsed["shortUrl"],
                                         EmbedUrl = (string) parsed["embedUrl"],
                                         Duration = new TimeSpan(0, 0, (int) parsed["duration"]),
                                         AlbumArtistName = (string) parsed["albumArtistName"],
                                         AlbumArtistKey = (string) parsed["albumArtistKey"],
                                         CanDownload = (bool) parsed["canDownload"],
                                         CanDownloadAlbumOnly = (bool) parsed["canDownloadAlbumOnly"]
                                     };
                    if (parsed.TryGetValue("playCount", out playCount))
                        ((RdioTrack)rdioObject).PlayCount = (int) playCount;
                    break;
                case RdioType.User:
                    JToken username, lastSongPlayed, displayName, trackCount, lastSongPlayTime;
                    rdioObject = new RdioUser
                                     {
                                         FirstName = (string) parsed["firstName"],
                                         LastName = (string) parsed["lastName"],
                                         Name = (string) parsed["firstName"] + " " + (string)parsed["lastName"],
                                         LibraryVersion = (long) parsed["libraryVersion"],
                                         Gender = (string) parsed["gender"]
                                     };
                    if (parsed.TryGetValue("username", out username))
                        ((RdioUser)rdioObject).Username = (string)username;
                    if (parsed.TryGetValue("lastSongPlayed", out lastSongPlayed))
                        ((RdioUser)rdioObject).LastSongPlayed = (string)lastSongPlayed;
                    if (parsed.TryGetValue("displayName", out displayName))
                        ((RdioUser)rdioObject).DisplayName = (string)displayName;
                    if (parsed.TryGetValue("trackCount", out trackCount))
                        ((RdioUser)rdioObject).TrackCount = (int)trackCount;
                    if (parsed.TryGetValue("lastSongPlayTime", out lastSongPlayTime))
                        ((RdioUser)rdioObject).LastSongPlayTime = (DateTime)lastSongPlayTime;
                    break;
                default:
                    break;
            }
            if (rdioObject != null)
            {
                rdioObject.Key = (string)parsed["key"];
                rdioObject.Url = (string)parsed["url"];
                rdioObject.Icon = (string)parsed["icon"];
                rdioObject.BaseIcon = (string)parsed["baseIcon"];
                rdioObject.RdioType = rdioType;
            }

            return rdioObject;
        }

        #endregion
    }
}

