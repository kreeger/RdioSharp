using Microsoft.VisualStudio.TestTools.UnitTesting;
using RdioSharp;

namespace RdioSharp.Test
{
    [TestClass]
    public class RdioSharpTest
    {
        private readonly RdioManager _manager;

        public RdioSharpTest()
        {
            var consumerKey = string.Empty;
            var consumerSecret = string.Empty;
            _manager = new RdioManager(consumerKey, consumerSecret);
        }

        [TestMethod]
        public void FindUserTest()
        {
            var user = _manager.FindUser(vanityName: "bradwalters");
        }
    }
}
