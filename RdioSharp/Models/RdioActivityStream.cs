using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioActivityStream : IRdioBaseObject
    {
        public long LastId { get; set; }
        public RdioUser User { get; set; }
        public IList<RdioActivityItem> Updates { get; set; }

        internal RdioActivityStream() { }
    }
}
