using System;

namespace RdioSharp.Models
{
    public class RdioUser : IRdioObject
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string BaseIcon { get; set; }
        public RdioType RdioType { get; set; }
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
