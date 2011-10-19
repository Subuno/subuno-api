using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Subuno
{
    public class SubunoApi
    {
        public static string SUBUNO_SERVER_URI = "https://api.subuno.com/v1/";

        private static string _apikey;
        private static string _server_uri;

        public static string Run(string apikey, Dictionary<string, string> data)
        {
            return Run(apikey, data, SUBUNO_SERVER_URI);
        }

        public static string Run(string apikey, Dictionary<string, string> data, string serverUri)
        {
            SetAuthenticationInfo(apikey, serverUri);
            return CallServer(data);
        }

        private static string CallServer(IDictionary<string, string> data)
        {
            if (string.IsNullOrEmpty(_apikey))
            {
                throw new SubunoApiError("API key not set.");
            }

            HttpWebRequest request;
            HttpWebResponse response = null;
            StreamReader reader;
            StringBuilder sbSource;

            //add apikey to the data packet.
            data.Add("apikey", _apikey);

            // serialize data.
            string urlencoding = string.Empty;
            urlencoding = data.Aggregate(urlencoding, (current, pair) => current + "&" + HttpUtility.UrlEncode(pair.Key, Encoding.GetEncoding("UTF-8")) + "=" + HttpUtility.UrlEncode(pair.Value, Encoding.GetEncoding("UTF-8")));
            urlencoding = urlencoding.Substring(1); // remove first &

            UriBuilder builder = null;
            try
            {
                builder = new UriBuilder(new Uri(_server_uri + "?" + urlencoding));
            }
            catch (UriFormatException e)
            {
                Console.WriteLine(e.ToString());
            }

            try
            {
                // Create and initialize the web request  
                request = WebRequest.Create(builder.ToString()) as HttpWebRequest;

                // Get response  
                response = request.GetResponse() as HttpWebResponse;

                if (request.HaveResponse && response != null)
                {
                    // Get the response stream  
                    reader = new StreamReader(response.GetResponseStream());

                    // Read it into a StringBuilder  
                    sbSource = new StringBuilder(reader.ReadToEnd());

                    // Console application output  
                    return sbSource.ToString();
                }
            }
            catch (WebException wex)
            {
                // This exception will be raised if the server didn't return 200 - OK  
                // Try to retrieve more information about the network error  
                if (wex.Response != null)
                {
                    using (HttpWebResponse errorResponse = (HttpWebResponse)wex.Response)
                    {
                        Console.WriteLine(
                            "The server returned '{0}' with the status code {1} ({2:d}).",
                            errorResponse.StatusDescription, errorResponse.StatusCode,
                            errorResponse.StatusCode);
                    }
                }
            }
            finally
            {
                if (response != null) { response.Close(); }
            }

            return string.Empty;
        }

        private static void SetAuthenticationInfo(string apikey, string serverUri)
        {
            _apikey = apikey;
            _server_uri = serverUri;
        }
    }

    public class SubunoApiError : Exception
    {
        public SubunoApiError(string message) : base(message) { }
    }
}
