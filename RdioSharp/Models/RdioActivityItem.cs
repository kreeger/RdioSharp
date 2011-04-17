using System;
using System.Collections.Generic;

using RdioSharp.Enum;

namespace RdioSharp.Models
{
    public class RdioActivityItem
    {
        public RdioUser Owner { get; set; }
        public DateTime Date{ get; set; }
        public RdioUpdateType UpdateType
        {
            get { return (RdioUpdateType)System.Enum.ToObject(typeof(RdioUpdateType), UpdateTypeId); }
        }
        public int UpdateTypeId { get; set; }
        public IList<RdioAlbum> Albums { get; set; }
        public IRdioObject ReviewedItem { get; set; }
        public string Comment { get; set; }
		
		internal RdioActivityItem() { }
    }
}
