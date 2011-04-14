namespace RdioSharp.Models
{
    public interface IRdioMusicObject : IRdioObject
    {
        string Artist { get; }
        string ArtistUrl { get; }
        string ArtistKey { get; }
        bool IsExplicit { get; }
        bool IsClean { get; }
        string Price { get; }
        bool CanStream { get; }
        bool CanSample { get; }
        bool CanTether { get; }
        string ShortUrl { get; }
        string EmbedUrl { get; }
        int Duration { get; }
    }
}
