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

        /// <summary>
        /// NOTE: ADD INITIALIZER FOR DICT, TOO; ALSO, PARAM DOCS
        /// </summary>
        /// <param name="key"></param>
        /// <param name="url"></param>
        /// <param name="icon"></param>
        /// <param name="baseIcon"></param>
        /// <param name="rdioType"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="libraryVersion"></param>
        /// <param name="gender"></param>
        /// <param name="username"></param>
        /// <param name="lastSongPlayed"></param>
        /// <param name="displayName"></param>
        /// <param name="trackCount"></param>
        /// <param name="lastSongPlayTime"></param>
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
