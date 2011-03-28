using System;
using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioUser : IRdioObject
    {
        public string Key { get; private set; }
        public string Name { get { return FirstName + " " + LastName; } }
        public string Url { get; private set; }
        public string Icon { get; private set; }
        public string BaseIcon { get; private set; }
        public RdioType RdioType { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long LibraryVersion { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public string LastSongPlayed { get; set; }
        public string DisplayName { get; set; }
        public int TrackCount { get; set; }
        public DateTime LastSongPlayTime { get; set; }

        private RdioUser(string key, string url, string icon, string baseIcon, RdioType rdioType,
                      string firstName, string lastName, long libraryVersion, string gender,
                      string username = null, string lastSongPlayed = null, string displayName = null,
                      int trackCount = -1, DateTime lastSongPlayTime = new DateTime())
        {
            Key = key;
            Url = url;
            Icon = icon;
            BaseIcon = baseIcon;
            RdioType = rdioType;
            FirstName = firstName;
            LastName = lastName;
            LibraryVersion = libraryVersion;
            Gender = gender;
            Username = username;
            LastSongPlayed = lastSongPlayed;
            DisplayName = displayName;
            TrackCount = trackCount;
            LastSongPlayTime = lastSongPlayTime;
        }

        public RdioUser(IDictionary<string, object> dictionary)
        {
            Key = (string)dictionary["key"];
            Url = (string)dictionary["url"];
            Icon = (string)dictionary["icon"];
            BaseIcon = (string)dictionary["baseIcon"];
            RdioType = (RdioType)dictionary["rdioType"];
            FirstName = (string)dictionary["firstName"];
            LastName = (string)dictionary["lastName"];
            LibraryVersion = (long)dictionary["libraryVersion"];
            Gender = (string)dictionary["gender"];
            object username, lastSongPlayed, displayName, trackCount, lastSongPlayTime;
            if (dictionary.TryGetValue("username", out username))
                Username = (string)username;
            if (dictionary.TryGetValue("lastSongPlayed", out lastSongPlayed))
                LastSongPlayed = (string)lastSongPlayed;
            if (dictionary.TryGetValue("displayName", out displayName))
                DisplayName = (string)displayName;
            if (dictionary.TryGetValue("trackCount", out trackCount))
                TrackCount = (int)trackCount;
            if (dictionary.TryGetValue("lastSongPlayTime", out lastSongPlayTime))
                LastSongPlayTime = (DateTime) lastSongPlayTime;
        }
    }
}
