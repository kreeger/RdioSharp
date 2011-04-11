using System;
using System.Collections.Generic;

using RdioSharp.Enum;

namespace RdioSharp.Models
{
    public class RdioActivityItem
    {
        public RdioUser Owner { get; private set; }
        public DateTime Date{ get; private set; }
        public RdioUpdateType UpdateType { get; private set; }
        public int UpdateTypeId { get; private set; }
        public IList<RdioAlbum> Albums { get; private set; }
        public IRdioObject ReviewedItem { get; private set; }
        public string Comment { get; private set; }
		
		internal RdioActivityItem() { }
    }
}
