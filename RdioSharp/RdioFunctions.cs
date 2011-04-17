using System;
using System.Collections.Generic;
using System.Linq;

using RdioSharp.Enum;
using RdioSharp.Models;

namespace RdioSharp
{
	public static class RdioFunctions
    {
        public static RdioType ParseRdioType(string input)
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

        public static IRdioObject ConvertDictionaryToRdioObject(IDictionary<string, object> d,
                                                                RdioType type = RdioType.Unknown)
        {
            IRdioObject rdioObject = null;
            if (type == RdioType.Unknown) type = ParseRdioType((string) d["type"]);
            switch (type)
            {
                case RdioType.CollectionAlbum:
                case RdioType.Album:
                    var album = new RdioAlbum
                    {
                        Artist = (string)d["artist"],
                        ArtistKey = (string)d["artistKey"],
                        ArtistUrl = (string)d["artistUrl"],
                        BaseIcon = (string)d["baseIcon"],
                        CanSample = (bool)d["canSample"],
                        CanStream = (bool)d["canStream"],
                        CanTether = (bool)d["canTether"],
                        Duration = (int)d["duration"],
                        EmbedUrl = (string)d["embedUrl"],
                        Icon = (string)d["icon"],
                        IsClean = (bool)d["isClean"],
                        IsExplicit = (bool)d["isExplicit"],
                        Key = (string)d["key"],
                        Length = (int)d["length"],
                        Name = (string)d["name"],
                        Price = (string)d["price"],
                        ReleaseDate = DateTime.Parse((string)d["releaseDate"]),
                        ShortUrl = (string)d["shortUrl"],
                        Type = (string)d["type"],
                        Url = (string)d["url"]
                    };
                    object keys;
                    if (d.TryGetValue("trackKeys", out keys))
                        album.TrackKeys = new List<object>((object[])keys).Cast<string>().ToList();
                    rdioObject = album;
                    break;
                case RdioType.CollectionArtist:
                case RdioType.Artist:
                    var artist = new RdioArtist
                    {
                        BaseIcon = (string)d["baseIcon"],
                        HasRadio = (bool)d["hasRadio"],
                        Icon = (string)d["icon"],
                        Key = (string)d["key"],
                        Name = (string)d["name"],
                        ShortUrl = (string)d["shortUrl"],
                        Length = (int)d["length"],
                        Type = (string)d["type"],
                        Url = (string)d["url"]
                    };
                    object albumCount;
                    if (d.TryGetValue("albumCount", out albumCount))
                        artist.AlbumCount = (int)albumCount;
                    rdioObject = artist;
                    break;
                case RdioType.Playlist:
                    var playlist = new RdioPlaylist
                    {
                        BaseIcon = (string)d["baseIcon"],
                        EmbedUrl = (string)d["embedUrl"],
                        Icon = (string)d["icon"],
                        Key = (string)d["key"],
                        LastUpdated = (decimal)d["lastUpdated"],
                        Length = (int)d["length"],
                        Name = (string)d["name"],
                        Owner = (string)d["owner"],
                        OwnerIcon = (string)d["ownerIcon"],
                        OwnerKey = (string)d["ownerKey"],
                        OwnerUrl = (string)d["ownerUrl"],
                        ShortUrl = (string)d["shortUrl"],
                        Type = (string)d["type"],
                        Url = (string)d["url"]
                    };
                    rdioObject = playlist;
                    break;
                case RdioType.Track:
                    var track = new RdioTrack
                    {
                        Album = (string)d["album"],
                        AlbumArtistKey = (string)d["albumArtistKey"],
                        AlbumArtistName = (string)d["albumArtist"],
                        AlbumKey = (string)d["albumKey"],
                        AlbumUrl = (string)d["albumUrl"],
                        Artist = (string)d["artist"],
                        ArtistKey = (string)d["artistKey"],
                        ArtistUrl = (string)d["artistUrl"],
                        BaseIcon = (string)d["baseIcon"],
                        CanDownload = (bool)d["canDownload"],
                        CanDownloadAlbumOnly = (bool)d["canDownloadAlbumOnly"],
                        CanSample = (bool)d["canSample"],
                        CanStream = (bool)d["canStream"],
                        CanTether = (bool)d["canTether"],
                        Duration = (int)d["duration"],
                        EmbedUrl = (string)d["embedUrl"],
                        Icon = (string)d["icon"],
                        IsClean = (bool)d["isClean"],
                        IsExplicit = (bool)d["isExplicit"],
                        Key = (string)d["key"],
                        Name = (string)d["name"],
                        Price = (string)d["price"],
                        ShortUrl = (string)d["shortUrl"],
                        Type = (string)d["type"],
                        Url = (string)d["url"]
                    };
                    object playCount;
                    if (d.TryGetValue("playCount", out playCount))
                        track.PlayCount = (int)playCount;
                    rdioObject = track;
                    break;
                case RdioType.User:
                    var user = new RdioUser
                    {
                        BaseIcon = (string)d["baseIcon"],
                        FirstName = (string)d["firstName"],
                        Gender = (string)d["gender"],
                        Icon = (string)d["icon"],
                        Key = (string)d["key"],
                        LastName = (string)d["lastName"],
                        LibraryVersion = (int)d["libraryVersion"],
                        Type = (string)d["type"],
                        Url = (string)d["url"]
                    };
                    object displayName, lastSongPlayed, lastSongPlayTime, trackCount, username;
                    if (d.TryGetValue("displayName", out displayName))
                        user.DisplayName = (string)displayName;
                    if (d.TryGetValue("lastSongPlayed", out lastSongPlayed))
                        user.LastSongPlayed = (string)lastSongPlayed;
                    if (d.TryGetValue("lastSongPlayTime", out lastSongPlayTime))
                        user.LastSongPlayTime = (DateTime)lastSongPlayTime;
                    if (d.TryGetValue("trackCount", out trackCount))
                        user.TrackCount = (int)trackCount;
                    if (d.TryGetValue("username", out username))
                        user.Username = (string)username;
                    rdioObject = user;
                    break;
                default:
                    break;
            }
            return rdioObject;
        }

        public static RdioResultSet ConvertListToRdioResultSet(IEnumerable<object> results)
        {
            var resultSet = new RdioResultSet();
            foreach (var result in results.Cast<IDictionary<string, object>>())
            {
                var rdioType = ParseRdioType((string) result["type"]);
                var rdioObject = ConvertDictionaryToRdioObject(result, rdioType);
                switch (rdioType)
                {
                    case RdioType.Album:
                        resultSet.Albums.Add(rdioObject as RdioAlbum);
                        break;
                    case RdioType.Artist:
                        resultSet.Artists.Add(rdioObject as RdioArtist);
                        break;
                    case RdioType.Playlist:
                        resultSet.Playlists.Add(rdioObject as RdioPlaylist);
                        break;
                    case RdioType.Track:
                        resultSet.Tracks.Add(rdioObject as RdioTrack);
                        break;
                    case RdioType.User:
                        resultSet.Users.Add(rdioObject as RdioUser);
                        break;
                    default:
                        break;
                }
            }

            return resultSet;
        }

        public static RdioResultSet ConvertDictionaryToRdioResultSet(IEnumerable<KeyValuePair<string, object>> results)
        {
            var resultSet = new RdioResultSet();
            foreach (var kvp in results)
            {
                var rdioType = ParseRdioType(kvp.Key.Substring(0, 1));
                var d = (Dictionary<string,object>)kvp.Value;
                if (d == null) continue;
                var rdioObject = ConvertDictionaryToRdioObject(d, rdioType);
                switch (rdioType)
                {
                    case RdioType.Album:
                        resultSet.Albums.Add(rdioObject as RdioAlbum);
                        break;
                    case RdioType.Artist:
                        resultSet.Artists.Add(rdioObject as RdioArtist);
                        break;
                    case RdioType.Playlist:
                        resultSet.Playlists.Add(rdioObject as RdioPlaylist);
                        break;
                    case RdioType.Track:
                        resultSet.Tracks.Add(rdioObject as RdioTrack);
                        break;
                    case RdioType.User:
                        resultSet.Users.Add(rdioObject as RdioUser);
                        break;
                    default:
                        break;
                }
            }
            return resultSet;
        }

        public static RdioActivityStream ConvertDictionaryToRdioActivityStream(IDictionary<string, object> d)
        {
            var stream = new RdioActivityStream
                             {
                                 User =
                                     ConvertDictionaryToRdioObject(d["user"] as IDictionary<string, object>) as RdioUser,
                                 LastId = (int) d["last_id"],
                                 Updates = new List<RdioActivityItem>()
                             };
            var updates = new List<object>((object[]) d["updates"]);
            foreach (IDictionary<string, object> u in updates)
            {
                var item = new RdioActivityItem
                               {
                                   Date = DateTime.Parse((string) u["date"]),
                                   Owner =
                                       ConvertDictionaryToRdioObject(u["owner"] as IDictionary<string, object>) as
                                       RdioUser,
                                   UpdateTypeId = (int) u["update_type"]
                               };
                object albums, comment, reviewedItem;
                if (u.TryGetValue("albums", out albums))
                {
                    item.Albums = new List<RdioAlbum>();
                    var albumObjects = new List<object>((object[])albums);
                    foreach (IDictionary<string, object> a in albumObjects)
                    {
                        item.Albums.Add(ConvertDictionaryToRdioObject(a) as RdioAlbum);
                    }
                }
                if (u.TryGetValue("comment", out comment))
                    item.Comment = (string)comment;
                if (u.TryGetValue("reviewedItem", out reviewedItem))
                    item.ReviewedItem = ConvertDictionaryToRdioObject((IDictionary<string, object>) reviewedItem);

                stream.Updates.Add(item);
            }
            return stream;
        }

        public static DateTime ParseStringToDateTime(string input)
        {
            var dateList = input.Split('-');
            return new DateTime(int.Parse(dateList[0]), int.Parse(dateList[1]), int.Parse(dateList[2]));
        }

        public static DateTime DeUnixify(this double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public static string Pluralize(this RdioType type)
        {
            return type + "s";
        }

        public static bool Boolify(this object obj)
        {
            if (obj == null) return false;
            if (obj is int && (int)obj == 0) return false;
            if (obj is long && (long)obj == 0) return false;
            if (obj is decimal && (decimal)obj == 0) return false;
            if (obj is double && (double)obj == 0) return false;
            if (obj is string && (string)obj == string.Empty) return false;
            if (obj is DateTime && ((DateTime)obj == DateTime.MinValue || (DateTime)obj == DateTime.MaxValue))
                return false;
            if (obj is IEnumerable<object> && ((IEnumerable<object>)obj).Count() == 0) return false;
            return true;
        }
    }
}

