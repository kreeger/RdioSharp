using System.Collections.Generic;

namespace RdioSharp.Models
{
    public class RdioActivityStream
    {
        public long LastId { get; private set; }
        public RdioUser User { get; private set; }
        public IList<RdioActivityItem> Updates { get; private set; }

        internal RdioActivityStream() { }
    }
}
