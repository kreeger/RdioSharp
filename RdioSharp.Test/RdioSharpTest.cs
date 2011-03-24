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
            var consumerKey = "z4m9gvhwxzjpmqan2hb4zvfx";
            var consumerSecret = "t6G8HrRDGT";
            _manager = new RdioManager(consumerKey, consumerSecret);
        }

        [TestMethod]
        public void FindUserTest()
        {
            var user = _manager.FindUser(vanityName: "bradwalters");
        }

        [TestMethod]
        public void CurrentUserTest()
        {
            _manager.GenerateRequestTokenAndLoginUrl();
            var user = _manager.FindUser(vanityName: "bradwalters");
        }
    }
}
