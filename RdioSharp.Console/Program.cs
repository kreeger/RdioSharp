using System.Diagnostics;
using RdioSharp.Enum;

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

			System.Console.WriteLine("Getting user.");
        	var user = manager.CurrentUser(new[] {"isSubscriber", "isTrial", "isUnlimited"});
        	System.Console.WriteLine("Current user key is : {0}.\nSubscriber: {1}\nUnlimited: {2}\nTrial: {3}", user.Key, user.IsSubscriber, user.IsUnlimited, user.IsTrial);

			//HMM not sure if this sort type from Rdio is actually working - KevM
			System.Console.WriteLine("Get top 10 artists in user's collection by play count.");
        	var artists = manager.GetArtistsInCollection(user.Key, 0, 10, RdioSortBy.PlayCount);
        	foreach (var artist in artists)
        	{
				System.Console.WriteLine("Artist: '{0}' (key: {1})", artist.Name, artist.Key);
        	}

            System.Console.WriteLine("Getting activity stream for current user.");
            var rdio = manager.GetActivityStream(user.Key);
            foreach (var item in rdio.Updates)
            {
                System.Console.WriteLine(string.Format("Update by {0} at {1}", item.Owner.Name, item.Owner.Key));
                System.Console.WriteLine(string.Format("Update type: {0}", item.UpdateType));
                if (item.Albums.Boolify())
                    foreach (var album in item.Albums)
                    {
                        System.Console.WriteLine(string.Format("Album: '{0}' by {1}", album.Name, album.Artist));
                    }
                if (item.ReviewedItem.Boolify())
                    System.Console.WriteLine(string.Format("Reviewed '{0}': {1}", item.ReviewedItem.RdioType,
                                                           item.ReviewedItem.Name));
                if (item.Comment.Boolify())
                    System.Console.WriteLine(string.Format("Comment: {0}", item.Comment));
            }
            System.Console.WriteLine("Press any key to continue.");
            System.Console.ReadKey();
        }
    }
}
