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

            var lists = manager.GetPlaylists();
            System.Console.WriteLine(lists.Owned.Count);
            //System.Console.WriteLine("Getting activity.");
            //var activity = manager.GetActivityStream(manager.CurrentUser().Key);
            System.Console.WriteLine("Press any key to continue.");
            System.Console.ReadKey();
        }
    }
}
