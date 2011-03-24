using System.Diagnostics;
namespace RdioSharp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumerKey = Constants.ConsumerKey;
            var consumerSecret = Constants.ConsumerSecret;
            var accessKey = Constants.AccessKey;
            var accessSecret = Constants.AccessSecret;
            var _manager = new RdioManager(consumerKey, consumerSecret, accessKey, accessSecret);

            System.Console.WriteLine("Finding user.");
            var user = _manager.FindUser(email: "benjaminkreeger@gmail.com");
            System.Console.WriteLine(user);

            if (_manager.IsAuthorized)
            {
                System.Console.WriteLine("Getting request token.");
                _manager.GenerateRequestTokenAndLoginUrl();
                System.Console.WriteLine(string.Format("Authorize at this url: {0}", _manager.LoginUrl));
                var p = new Process { StartInfo = new ProcessStartInfo(_manager.LoginUrl) };
                p.Start();
                System.Console.Write("What is the pin number? ");
                var pin = System.Console.ReadLine();

                System.Console.WriteLine(string.Format("Authorizing for pin # {0}.", pin));
                _manager.Authorize(pin);
                System.Console.WriteLine("Now have credentials!\nAccess key: {0}\nAccess key secret: {1}", _manager.AccessKey, _manager.AccessKeySecret);
            }

            System.Console.WriteLine("Getting current user.");
            var currentUser = _manager.CurrentUser();
            System.Console.WriteLine(currentUser);
            System.Console.ReadKey();
        }
    }
}
