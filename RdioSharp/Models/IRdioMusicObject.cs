using System;

namespace RdioSharp.Models
{
    public interface IRdioMusicObject : IRdioObject
    {
        string ArtistName { get; }
        string ArtistUrl { get; }
        string ArtistKey { get; }
        bool IsExplicit { get; }
        bool IsClean { get; }
        decimal Price { get; }
        bool CanStream { get; }
        bool CanSample { get; }
        bool CanTether { get; }
        string ShortUrl { get; }
        string EmbedUrl { get; }
        TimeSpan Duration { get; }
    }
}
