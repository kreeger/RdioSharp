using System.ComponentModel;

namespace RdioSharp.Enum
{
    public enum RdioUpdateType
    {
        [Description("Music added to collection")]
        MusicAddedToCollection = 0,
        [Description("Music added to playlist")]
        MusicAddedToPlaylist = 1,
        [Description("Friend added")]
        FriendAdded = 3,
        [Description("User joined")]
        UserJoined = 5,
        [Description("Comment added to track")]
        CommentAddedToTrack = 6,
        [Description("Comment added to album")]
        CommentAddedToAlbum = 7,
        [Description("Comment added to artist")]
        CommentAddedToArtist = 8,
        [Description("Comment added to playlist")]
        CommentAddedToPlaylist = 9,
        [Description("Music added via match collection")]
        MusicAddedViaMatchCollection = 10,
        [Description("User subscribed to Rdio")]
        UserSubscribedToRdio = 11,
        [Description("Music synced to mobile")]
        MusicSyncedToMobile = 12
    }
}
