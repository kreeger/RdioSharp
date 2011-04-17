# RdioSharp

## About

RdioSharp is a .NET library for accessing the [Rdio API](http://developer.rdio.com/), using OAuth. Major props to [Shannon Whitley's blog post about Twitter and OAuth](http://goo.gl/WdHzi) for helping me figure out some no-nonsense, no-bloat ways to connect and authorize via OAuth.

Function names and objects will follow Rdio's API specs for [methods](http://developer.rdio.com/docs/read/rest/methods) and [object types](http://developer.rdio.com/docs/read/rest/types) as closely as possible, making this a (hopefully) easy-to-use library.

## Notes

I've been developing this with the .NET Framework v4.0, so it's the bee's knees; just know I haven't tested it with anything older yet. I will. I promise.

Also, this is a shining example of work-in-progress. You have been warned.

## Requirements

 * Microsoft .NET Framework 4

## Usage

*More coming soon*. I'll be implementing test cases in the `RdioSharpTest` class, too, which ought to help with real-world usage examples.

    // Instantiate the manager.
    var manager = new RdioManager(consumerKey, consumerSecret, accessKey, accessSecret);
    
    // How to handle authorization.
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
    
    // Get the current user.
    var currentUser = manager.CurrentUser("s1250");
    System.Console.WriteLine(string.Format("Getting activity stream for {0}.", currentUser.Name));
    
    // Get the activity stream and flip through the contents of the activity stream.
    var rdio = manager.GetActivityStream(currentUser.Key);
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
	
## Version history

Because you all care.

 * **Version 0.1**: Initial release. Includes all API calls.