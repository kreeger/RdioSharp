using System;

namespace RdioSharp.Models
{
    public class User : RdioObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LibraryVersion { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public string LastSongPlayed { get; set; }
        public string DisplayName { get; set; }
        public int TrackCount { get; set; }
        public DateTime LastSongPlayTime { get; set; }

        internal User(string key, string url, string icon, string baseIcon, RdioType rdioType,
                      string firstName, string lastName, string libraryVersion, string gender,
                      string username = null, string lastSongPlayed = null, string displayName = null,
                      int trackCount = 0, DateTime lastSongPlayTime = new DateTime())
            : base(key, url, icon, baseIcon, rdioType)
        {
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
    }
}
