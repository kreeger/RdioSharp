using RdioSharp.Models;

namespace RdioSharp
{
    public interface IRdioManager
    {
    }
    
    internal class RdioManager : IRdioManager
    {
        private string _consumerKey;
        private string _consumerSecret;
        private string _accessKey;
        private string _accessSecret;

        private const string API_URI = "http://api.rdio.com/1/";

        internal RdioManager(string consumerKey = null, string consumerSecret = null,
                             string accessKey = null, string accessSecret = null)
        {
            SetCredentials(consumerKey, consumerSecret, accessKey, accessSecret);
        }

        /// <summary>
        /// Sets credentials for the Rdio API manager.
        /// </summary>
        /// <param name="consumerKey">The OAuth consumer key.</param>
        /// <param name="consumerSecret">The OAuth consumer secret.</param>
        /// <param name="accessKey">The OAuth access token key.</param>
        /// <param name="accessSecret">The OAuth access token key.</param>
        private void SetCredentials(string consumerKey = null, string consumerSecret = null,
                                string accessKey = null, string accessSecret = null)
        {
            if (!string.IsNullOrEmpty(consumerKey) && !string.IsNullOrEmpty(consumerSecret))
            {
                _consumerKey = consumerKey;
                _consumerSecret = consumerSecret;
            }

            if (!string.IsNullOrEmpty(accessKey) && !string.IsNullOrEmpty(accessSecret))
            {
                _accessKey = accessKey;
                _accessSecret = accessSecret;
            }
        }

        /// <summary>
        /// Find a user either by email address or by their username.
        /// Exactly one of email or vanityName must be supplied.
        /// </summary>
        /// <param name="email">An email address.</param>
        /// <param name="vanityName">A username.</param>
        /// <returns></returns>
        public User FindUser(string email = null, string vanityName = null)
        {
        }
    }
}
