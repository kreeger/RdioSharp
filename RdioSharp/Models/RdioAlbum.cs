using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using RdioSharp.Enum;

namespace RdioSharp.Models
{
    [DataContract]
    public class RdioAlbum : IRdioMusicObject
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
        public string Type { get; set; }
        public RdioType RdioType { get { return RdioFunctions.ParseRdioType(Type); } }
        [DataMember]
        public string Artist { get; set; }
        [DataMember]
        public string ArtistUrl { get; set; }
        [DataMember]
        public string ArtistKey { get; set; }
        [DataMember]
        public bool IsExplicit { get; set; }
        [DataMember]
        public bool IsClean { get; set; }
        [DataMember(Name = "Length")]
        public int Length { get; set; }
        [DataMember]
        public string Price { get; set; }
        [DataMember]
        public bool CanStream { get; set; }
        [DataMember]
        public bool CanSample { get; set; }
        [DataMember]
        public bool CanTether { get; set; }
        [DataMember]
        public string ShortUrl { get; set; }
        [DataMember]
        public string EmbedUrl { get; set; }
        [DataMember]
        public int Duration { get; set; }
        [DataMember]
        public DateTime ReleaseDate { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public IList<string> TrackKeys { get; set; }
    }
}
