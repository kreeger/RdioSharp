using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RdioSharp.Models
{
    [DataContract]
    public class RdioPlaylistSet : IRdioBaseObject
    {
        [DataMember(Name = "Owned")]
        public IList<RdioPlaylist> Owned { get; set; }
        [DataMember(Name = "Collab")]
        public IList<RdioPlaylist> Collab { get; set; }
        [DataMember(Name = "Subscribed")]
        public IList<RdioPlaylist> Subscribed { get; set; }
    }
}
