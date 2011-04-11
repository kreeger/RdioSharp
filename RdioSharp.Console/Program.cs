using System.Collections.Generic;
using System.Diagnostics;
using RdioSharp.Enum;
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

            System.Console.WriteLine("Searching.");
            var result = manager.Search(query: "Rhianna", types: new List<RdioType> {RdioType.Album}, extras: new List<string>{"trackKeys"});
            System.Console.WriteLine(result.Albums[1].Name);
            System.Console.WriteLine(result.Albums[1].TrackKeys);
            var added = manager.AddToCollection(result.Albums[1].TrackKeys);
            System.Console.WriteLine("Press any key to continue.");
            System.Console.ReadKey();
        }
    }
}
