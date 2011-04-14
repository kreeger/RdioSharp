using System;
using System.Runtime.Serialization;

using RdioSharp.Enum;

namespace RdioSharp.Models
{
    [DataContract]
    public class RdioUser : IRdioObject
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Icon { get; set; }
        [DataMember]
        public string BaseIcon { get; set; }
        [DataMember]
        public RdioType RdioType { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public long LibraryVersion { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string Username { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string LastSongPlayed { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string DisplayName { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public int TrackCount { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public DateTime LastSongPlayTime { get; set; }

        public string Name
        {
            get { return FirstName + " " + LastName; }
            set { Name = value; }
        }
    }
}
