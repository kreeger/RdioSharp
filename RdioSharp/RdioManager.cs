using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using RdioSharp.Models;

using System.Web;
using System;
using System.Text;

namespace RdioSharp
{
    public interface IRdioManager
    {
    }
    
    public class RdioManager : IRdioManager
    {

        #region Public properties.

        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }
        public string CallBackUrl { get; private set; }
        public string RequestToken { get; private set; }
        public string RequestTokenSecret { get; private set; }
        public string LoginUrl { get; set; }
        public string AccessKey { get; private set; }
        public string AccessKeySecret { get; private set; }
        public string OAuthVerifier { get; private set; }
        public bool IsAuthorized { get { return !string.IsNullOrEmpty(RequestTokenSecret); } }

        #endregion

        #region Constants.

        private const string API_URL = "http://api.rdio.com/1/";
        private const string REQUEST_TOKEN_URL = "http://api.rdio.com/oauth/request_token";
        private const string ACCESS_TOKEN_URL = "http://api.rdio.com/oauth/access_token";
        private const string REQUEST_METHOD = "POST";

        #endregion

        #region Constructor.

        /// <summary>
        /// NOTE: SHOULD I REPLICATE FUNCTIONALITY OF OAUTH.TOKEN IN OAUTH2?
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="accessKey"></param>
        /// <param name="accessSecret"></param>
        /// <param name="callbackUrl"></param>
        public RdioManager(string consumerKey = null, string consumerSecret = null,
                           string accessKey = null, string accessSecret = null, string callbackUrl = null)
        {
            CallBackUrl = !string.IsNullOrEmpty(callbackUrl) ? callbackUrl : "oob";
            SetCredentials(consumerKey, consumerSecret, accessKey, accessSecret);
        }

        #endregion

        #region Manager/OAuth management methods

        /// <summary>
        /// Sets credentials for the Rdio API manager.
        /// </summary>
        /// <param name="consumerKey">The OAuth consumer key.</param>
        /// <param name="consumerSecret">The OAuth consumer secret.</param>
        /// <param name="accessKey">The OAuth access token key.</param>
        /// <param name="accessSecret">The OAuth access token key.</param>
        public void SetCredentials(string consumerKey = null, string consumerSecret = null,
                                   string accessKey = null, string accessSecret = null)
        {
            if (!string.IsNullOrEmpty(consumerKey) && !string.IsNullOrEmpty(consumerSecret))
            {
                ConsumerKey = consumerKey;
                ConsumerSecret = consumerSecret;
            }

            if (!string.IsNullOrEmpty(accessKey) && !string.IsNullOrEmpty(accessSecret))
            {
                AccessKey = accessKey;
                AccessKeySecret = accessSecret;
            }
        }

        /// <summary>
        /// Get the link to Rdio's authorization page for this application.
        /// </summary>
        /// <returns>The url with a valid request token, or a null string.</returns>
        public void GenerateRequestTokenAndLoginUrl()
        {
            var data = new NameValueCollection { { "oauth_callback", CallBackUrl } };

            var response = MakeWebRequest(REQUEST_TOKEN_URL, data);
            if (response.Length > 0)
            {
                //response contains token and token secret.  We only need the token.
                var qs = HttpUtility.ParseQueryString(response);

                if (qs["oauth_callback_confirmed"] != null)
                    if (qs["oauth_callback_confirmed"] != "true")
                        throw new Exception("OAuth callback not confirmed.");

                if (qs["oauth_token"] != null)
                {
                    LoginUrl = string.Format("{0}?oauth_token={1}", qs["login_url"], qs["oauth_token"]);
                    RequestToken = qs["oauth_token"];
                    RequestTokenSecret = qs["oauth_token_secret"];
                }
            }
        }

        /// <summary>
        /// Exchange the request token for an access token.
        /// </summary>
        /// <param name="oAuthVerifier">An oauth_verifier parameter is provided to the client either in the pre-configured callback URL</param>
        public void Authorize(string oAuthVerifier)
        {
            OAuthVerifier = oAuthVerifier;

            // Make a web request to get the access token; this will already sign it with our verifier
            // which is now no longer null.
            var response = MakeWebRequest(ACCESS_TOKEN_URL);

            if (response.Length <= 0) return;

            //Store the Token and Token Secret
            var qs = HttpUtility.ParseQueryString(response);

            if (!string.IsNullOrEmpty(qs["oauth_token"]) && !string.IsNullOrEmpty(qs["oauth_token_secret"]))
                SetCredentials(accessKey: qs["oauth_token"], accessSecret: qs["oauth_token_secret"]);
        }

        #endregion
        
        #region Rdio API methods

        /// <summary>
        /// Find a user either by email address or by their username.
        /// Exactly one of email or vanityName must be supplied.
        /// </summary>
        /// <param name="email">An email address.</param>
        /// <param name="vanityName">A username.</param>
        /// <returns></returns>
        public string FindUser(string email = null, string vanityName = null)
        {
            // Ugly. Fix it soon.
            var postData = new NameValueCollection { { "method", "findUser" } };
            if (email != null) postData.Add("email", email);
            else if (vanityName != null) postData.Add("vanityName", vanityName);

            // This will be in... JSON?
            return MakeWebRequest(API_URL, postData);
        }

        /// <summary>
        /// Get information about the currently logged in user.
        /// </summary>
        /// <param name="extras">An optional list of extra fields to send in.</param>
        /// <returns></returns>
        public string CurrentUser(IList<string> extras = null)
        {
            // Ugly. Fix it soon.
            var postData = new NameValueCollection { { "method", "currentUser" } };
            if (extras != null && extras.Count > 0)
            {
                var builder = new StringBuilder();
                for (var i = 0; i < extras.Count; i++)
                {
                    builder.Append(extras[i]);
                    if (i != extras.Count - 1) builder.Append(',');
                }
                postData.Add("extras", builder.ToString());
            }

            // This will be in... JSON?
            return MakeWebRequest(API_URL, postData);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Submit a web request using oAuth.
        /// </summary>
        /// <param name="url">The full url, including the querystring.</param>
        /// <param name="postData">Data to post, as a <see cref="NameValueCollection"/>.</param>
        /// <returns>The web server response.</returns>
        private string MakeWebRequest(string url, NameValueCollection postData = null)
        {
            string outUrl;
            string querystring;
            string postString;

            //Setup postData for signing.
            //Add the postData to the querystring.
            if (postData != null && postData.Count > 0)
            {
                //Decode the parameters and re-encode using the oAuth UrlEncode method.
                postString = string.Empty;
                foreach (var key in postData.AllKeys)
                {
                    if (postString.Length > 0) postString += "&";
                    // Decode as key.
                    postData[key] = HttpUtility.UrlDecode(postData[key]);
                    // Re-encode as key.
                    postData[key] = OAuth.UrlEncode(postData[key]);
                    // Append to outstring.
                    postString += key + "=" + postData[key];

                }
                if (url.IndexOf("?") > 0) url += "&";
                else url += "?";
                url += postString;
            }

            var uri = new Uri(url);

            var nonce = OAuth.GenerateNonce();
            var timeStamp = OAuth.GenerateTimeStamp();

            // Generate Signature
            // If we have an access key...
            var sig = OAuth.GenerateSignature(uri, ConsumerKey, ConsumerSecret, AccessKey ?? RequestToken,
                                              AccessKeySecret ?? RequestTokenSecret, CallBackUrl, OAuthVerifier,
                                              REQUEST_METHOD, timeStamp, nonce, out outUrl, out querystring);

            querystring += "&oauth_signature=" + OAuth.UrlEncode(sig);

            // Convert the querystring to postData
            postString = querystring;
            querystring = "";

            if (querystring.Length > 0) outUrl += "?";

            return DoWebRequest(outUrl + querystring, postString);
        }

        /// <summary>
        /// Web Request Wrapper
        /// </summary>
        /// <param name="url">Full url to the web resource</param>
        /// <param name="postData">Data to post in querystring format</param>
        /// <returns>The web server response.</returns>
        private static string DoWebRequest(string url, string postData)
        {
            string responseData = null;
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                // Set request details.
                webRequest.Method = REQUEST_METHOD;
                webRequest.ServicePoint.Expect100Continue = false;
                webRequest.ContentType = "application/x-www-form-urlencoded";

                // For getting through any proxy
                webRequest.Proxy = WebRequest.DefaultWebProxy;
                webRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;

                // Write the post data into the request's stream.
                var requestWriter = new StreamWriter(webRequest.GetRequestStream());
                try
                {
                    requestWriter.Write(postData);
                }
                finally
                {
                    requestWriter.Close();
                }

                // Process the data.
                StreamReader responseReader = null;
                Stream responseStream = null;

                try
                {
                    responseStream = webRequest.GetResponse().GetResponseStream();
                    if (responseStream != null)
                    {
                        responseReader = new StreamReader(responseStream);
                        responseData = responseReader.ReadToEnd();
                    }
                }
                finally
                {
                    if (responseStream != null) responseStream.Close();
                    if (responseReader != null) responseReader.Close();
                }
            }

            // NOTE: MAKE THIS A DICT!
            return responseData;
        }

        #endregion
    }
}
