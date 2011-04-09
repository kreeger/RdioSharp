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

        internal RdioUser() { }
    }
}
