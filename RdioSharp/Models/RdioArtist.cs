using System.Runtime.Serialization;

using RdioSharp.Enum;

namespace RdioSharp.Models
{
    [DataContract]
    public class RdioArtist : IRdioObject
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Icon { get; set; }
        [DataMember]
        public string BaseIcon { get; set; }
        [DataMember]
        public RdioType RdioType { get; set; }
        [DataMember(Name = "Length")]
        public int TrackCount { get; set; }
        [DataMember]
        public bool HasRadio { get; set; }
        [DataMember]
        public string ShortUrl { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public int AlbumCount { get; set; }
    }
}
