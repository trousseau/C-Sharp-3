using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FantasyTestConsole
{
    class Program
    {
        public string responseFromServer = "";
        public static void OAuthRequestYahooToken()
        {
            var oauth = new OAuth.Manager();
            var retUrl = "https://api.login.yahoo.com/oauth/v2/get_token";
            oauth["consumer_key"] = "dj0yJmk9N0pMNUdDbTRMNmFBJmQ9WVdrOVp6QlpRa2xETjJrbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD01MQ--";
            oauth["consumer_secret"] = "c1593b9fdc8a587aa848b79dae9727d8201423fc";
            oauth.AcquireRequestToken(retUrl, "GET");
        }

        public void GetAccessToken()
        {
            var responseFromServer = responseFromServer.Substring(1, responseFromServer.Length - 2);
            string consumerKey = "dj0yJmk9N0pMNUdDbTRMNmFBJmQ9WVdrOVp6QlpRa2xETjJrbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD01MQ--";
            string consumerSecret = "c1593b9fdc8a587aa848b79dae9727d8201423fc";

            string returnUrl = "http://www.yogihosting.com/TutorialCode/YahooOAuth2.0/yahoooauth2.aspx";

            /*Exchange authorization code for Access Token by sending Post Request*/
            Uri address = new Uri("https://api.login.yahoo.com/oauth2/get_token");

            // Create the web request
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            // Set type to POST
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] headerByte = System.Text.Encoding.UTF8.GetBytes(consumerKey + ":" + consumerSecret);
            string headerString = System.Convert.ToBase64String(headerByte);
            request.Headers["Authorization"] = "Basic " + headerString;

            // Create the data we want to send
            StringBuilder data = new StringBuilder();
            data.Append("client_id=" + consumerKey);
            data.Append("&client_secret=" + consumerSecret);
            data.Append("&redirect_uri=" + returnUrl);
            data.Append("&code=" + Request.QueryString["code"]);
            data.Append("&grant_type=authorization_code");

            // Create a byte array of the data we want to send
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());

            // Set the content length in the request headers
            request.ContentLength = byteData.Length;

            // Write data
            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }

            // Get response

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    responseFromServer = reader.ReadToEnd();
                    ShowReceivedData(responseFromServer);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void ShowReceivedData(string responseFromServer)
        {
            responseFromServer = responseFromServer.Substring(1, responseFromServer.Length - 2);
            string accessToken = "", xoauthYahooGuid = "", refreshToken = "", tokenType = "", expiresIn = "";
            string[] splitByComma = responseFromServer.Split(',');
            foreach (string value in splitByComma)
            {
                if (value.Contains("access_token"))
                {
                    string[] accessTokenSplitByColon = value.Split(':');
                    accessToken = accessTokenSplitByColon[1].Replace('"'.ToString(), "");
                }
                else if (value.Contains("xoauth_yahoo_guid"))
                {
                    string[] xoauthYahooGuidSplitByColon = value.Split(':');
                    xoauthYahooGuid = xoauthYahooGuidSplitByColon[1].Replace('"'.ToString(), "");
                }
                else if (value.Contains("refresh_token"))
                {
                    string[] refreshTokenSplitByColon = value.Split(':');
                    refreshToken = refreshTokenSplitByColon[1].Replace('"'.ToString(), "");
                }
                else if (value.Contains("token_type"))
                {
                    string[] tokenTypeSplitByColon = value.Split(':');
                    tokenType = tokenTypeSplitByColon[1].Replace('"'.ToString(), "");
                }
                else if (value.Contains("expires_in"))
                {
                    string[] expiresInSplitByColon = value.Split(':');
                    expiresIn = expiresInSplitByColon[1].Replace('"'.ToString(), "");
                }
            }
        }

        static void Main(string[] args)
        {
            OAuthRequestYahooToken();

        }
    }
}
