using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RdioSharp.Models
{
    [DataContract]
    public class RdioSearchResult
    {
        [DataMember]
        public int NumberResults { get; set; }
        [DataMember]
        public IList<IRdioObject> Results { get; set; }

        public RdioSearchResult() { }
    }
}
