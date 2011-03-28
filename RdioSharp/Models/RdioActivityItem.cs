using System;
using System.Collections.Generic;

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

        private RdioActivityItem(RdioUser owner, DateTime date, RdioUpdateType updateType, int updateTypeId,
                                 IList<RdioAlbum> albums = null, IRdioObject reviewedItem = null, string comment = null)
        {
            Owner = owner;
            Date = date;
            UpdateType = updateType;
            UpdateTypeId = updateTypeId;
            Albums = albums;
            ReviewedItem = reviewedItem;
            Comment = comment;
        }
    }
}
