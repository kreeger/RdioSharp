using System;

using RdioSharp.Enum;

namespace RdioSharp
{
	public static class RdioFunctions
    {
        public static RdioType ParseRdioType(string input)
        {
            switch (input)
            {
                case "r":
                case "ar": return RdioType.Artist;
                case "a":
                case "al": return RdioType.Album;
                case "t": return RdioType.Track;
                case "p": return RdioType.Playlist;
                case "s": return RdioType.User;
                default: return RdioType.Unknown;
            }
        }

        public static DateTime ParseStringToDateTime(string input)
        {
            var dateList = input.Split('-');
            return new DateTime(int.Parse(dateList[0]), int.Parse(dateList[1]), int.Parse(dateList[2]));
        }
    }
}

