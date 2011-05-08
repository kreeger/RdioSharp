using System;
using System.Runtime.Serialization;

using RdioSharp.Enum;

namespace RdioSharp.Models
{
    [DataContract]
    public class RdioTrack : IRdioMusicObject
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
		public string BigIcon { get; set; }
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
        public string Album { get; set; }
        [DataMember]
        public string AlbumUrl { get; set; }
        [DataMember]
        public string AlbumKey { get; set; }
        [DataMember]
        public bool IsExplicit { get; set; }
        [DataMember]
        public bool IsClean { get; set; }
		[DataMember]
		public bool IsOnCompilation { get; set; }
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
        public string AlbumArtistName { get; set; }
        [DataMember]
        public string AlbumArtistKey { get; set; }
        [DataMember]
        public bool CanDownload { get; set; }
        [DataMember]
        public bool CanDownloadAlbumOnly { get; set; }
        [DataMember]
        public int PlayCount { get; set; }

    	internal RdioTrack() { }
    }
}
