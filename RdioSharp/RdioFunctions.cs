using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json.Linq;
using RdioSharp.Models;

namespace RdioSharp
{
	public static class RdioFunctions
	{
        public static RdioType ParseRdioType(string input)
        {
            switch (input)
            {
                case "r": return RdioType.Artist;
                case "a": return RdioType.Album;
                case "t": return RdioType.Track;
                case "p": return RdioType.Playlist;
                case "s": return RdioType.User;
                default: return RdioType.Unknown;
            }
        }
		
		public static JObject CheckStatusAndGetResult(string input)
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
			
			return new RdioSearchResult {
				AlbumCount = (int)parsed["album_count"],
				ArtistCount = (int)parsed["artist_count"],
				ResultCount = (int)parsed["number_results"],
				PlaylistCount = (int)parsed["playlist_count"],
				TrackCount = (int)parsed["track_count"],
				Results = ParseJSONListStringToRdioObjectList((string)parsed["results"]).ToList()
			};
			
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
                                         Price = Decimal.Parse((string) parsed["price"]),
                                         CanStream = (bool) parsed["canStream"],
                                         CanSample = (bool) parsed["canSample"],
                                         CanTether = (bool) parsed["canTether"],
                                         ShortUrl = (string) parsed["shortUrl"],
                                         EmbedUrl = (string) parsed["embedUrl"],
                                         Duration = new TimeSpan(0, 0, (int) parsed["duration"]),
                                         ReleaseDate = DateTime.Parse((string) parsed["name"])
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
                    rdioObject = new RdioPlaylist(result);
                    break;
                case RdioType.Track:
                    JToken playCount;
                    result.Add("name", (string)parsed["name"]);
                    result.Add("artist", (string)parsed["artist"]);
                    result.Add("artistUrl", (string)parsed["artistUrl"]);
                    result.Add("artistKey", (string)parsed["artistKey"]);
                    result.Add("album", (string)parsed["album"]);
                    result.Add("albumUrl", (string)parsed["albumUrl"]);
                    result.Add("albumKey", (string)parsed["albumKey"]);
                    result.Add("isExplicit", (bool)parsed["isExplicit"]);
                    result.Add("isClean", (bool)parsed["isClean"]);
                    result.Add("price", Decimal.Parse((string)parsed["price"]));
                    result.Add("canStream", (bool)parsed["canStream"]);
                    result.Add("canSample", (bool)parsed["canSample"]);
                    result.Add("canTether", (bool)parsed["canTether"]);
                    result.Add("shortUrl", (string)parsed["shortUrl"]);
                    result.Add("embedUrl", (string)parsed["embedUrl"]);
                    result.Add("duration", new TimeSpan(0, 0, (int)parsed["duration"]));
                    result.Add("albumArtistName", (string)parsed["albumArtistName"]);
                    result.Add("albumArtistKey", (string)parsed["albumArtistKey"]);
                    result.Add("canDownload", (bool)parsed["canDownload"]);
                    result.Add("canDownloadAlbumOnly", (bool)parsed["canDownloadAlbumOnly"]);
                    if (parsed.TryGetValue("playCount", out playCount))
                        result.Add("playCount", (int)playCount);
                    rdioObject = new RdioTrack(result);
                    break;
                case RdioType.User:
                    JToken username, lastSongPlayed, displayName, trackCount, lastSongPlayTime;
                    result.Add("firstName", (string)parsed["firstName"]);
                    result.Add("lastName", (string)parsed["lastName"]);
                    result.Add("libraryVersion", (long)parsed["libraryVersion"]);
                    result.Add("gender", (string)parsed["gender"]);
                    if (parsed.TryGetValue("username", out username))
                        result.Add("username", (string)username);
                    if (parsed.TryGetValue("lastSongPlayed", out lastSongPlayed))
                        result.Add("lastSongPlayed", (string)lastSongPlayed);
                    if (parsed.TryGetValue("displayName", out displayName))
                        result.Add("displayName", (string)displayName);
                    if (parsed.TryGetValue("trackCount", out trackCount))
                        result.Add("trackCount", (int)trackCount);
                    if (parsed.TryGetValue("lastSongPlayTime", out lastSongPlayTime))
                        result.Add("lastSongPlayTime", (DateTime)lastSongPlayTime);
                    rdioObject = new RdioUser(result);
                    break;
                default:
                    break;
            }

            return rdioObject;
        }
	}
}

