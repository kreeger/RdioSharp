using System.Runtime.Serialization;

using RdioSharp.Enum;

namespace RdioSharp.Models
{
    [DataContract]
    public class RdioPlaylist: IRdioObject
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
        public string Type { get; set; }
        public RdioType RdioType { get { return RdioFunctions.ParseRdioType(Type); } }
        [DataMember]
        public string BaseIcon { get; set; }
        [DataMember]
        public int Length { get; set; }
        [DataMember]
        public string Owner { get; set; }
        [DataMember]
        public string OwnerUrl { get; set; }
        [DataMember]
        public string OwnerKey { get; set; }
        [DataMember]
        public string OwnerIcon { get; set; }
        [DataMember]
        public decimal LastUpdated { get; set; }
        [DataMember]
        public string ShortUrl { get; set; }
        [DataMember]
        public string EmbedUrl { get; set; }
    }
}
