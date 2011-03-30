using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RdioSharp.Models;

namespace RdioSharp.Console
{
    public static class Program
    {
        static void Main()
        {
            const string consumerKey = Constants.ConsumerKey;
            const string consumerSecret = Constants.ConsumerSecret;
            const string accessKey = Constants.AccessKey;
            const string accessSecret = Constants.AccessSecret;
            var manager = new RdioManager(consumerKey, consumerSecret, accessKey, accessSecret);

            if (!manager.IsAuthorized)
            {
                System.Console.WriteLine("Getting request token.");
                manager.GenerateRequestTokenAndLoginUrl();
                System.Console.WriteLine(string.Format("Authorize at this url: {0}", manager.LoginUrl));
                var p = new Process {StartInfo = new ProcessStartInfo(manager.LoginUrl)};
                p.Start();
                System.Console.Write("What is the pin number? ");
                var pin = System.Console.ReadLine();

                System.Console.WriteLine(string.Format("Authorizing for pin # {0}.", pin));
                manager.Authorize(pin);
                System.Console.WriteLine("Now have credentials!\nAccess key: {0}\nAccess key secret: {1}",
                                         manager.AccessKey, manager.AccessKeySecret);
            }

            System.Console.WriteLine("Getting album.");
            var search = manager.Get(new List<string> {"a154082", "r167947", "t1851076"}, new List<string> {"trackKeys"});
            foreach (var rdioObject in search)
            {
                System.Console.WriteLine(rdioObject.RdioType);
                System.Console.WriteLine(rdioObject.Name);
                if (rdioObject.GetType() == typeof(RdioAlbum))
                {
                    var album = rdioObject as RdioAlbum;
                    if (album == null) continue;
                    System.Console.WriteLine(album.ArtistKey);
                    System.Console.WriteLine(string.Format("{0}:{1}", album.Duration.Minutes, album.Duration.Seconds));
                    System.Console.WriteLine(string.Join(", ", album.TrackKeys));
                }
                else if (rdioObject.GetType() == typeof(RdioArtist))
                {
                    var artist = rdioObject as RdioArtist;
                    if (artist == null) continue;
                    System.Console.WriteLine(artist.Url);
                    System.Console.WriteLine(artist.ShortUrl);
                }
                else if (rdioObject.GetType() == typeof(RdioTrack))
                {
                    var track = rdioObject as RdioTrack;
                    if (track == null) continue;
                    System.Console.WriteLine(track.CanStream);
                    System.Console.WriteLine(track.CanTether);
                    System.Console.WriteLine(track.Url);
                    System.Console.WriteLine(track.ShortUrl);
                }
            }

            System.Console.WriteLine("Press any key to continue.");
            System.Console.ReadKey();
        }
    }
}
