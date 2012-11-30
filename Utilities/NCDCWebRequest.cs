using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NCDCWebService.Utilities
{
    public class NCDCWebRequest
    {
        public NCDCWebRequest(Uri uri, HTTPVerb hTTPVerb, string token, string userAgent = "")
        {
            if (uri == null)
                throw new ArgumentNullException("requestUri");
            // TODO: Complete member initialization
            this.RequestUri = uri;
            this.Verb = hTTPVerb;
            this.Token = token;
            this.UserAgent = userAgent;

            this.Parameters = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Parameters { get; private set; }
        private string UserAgent { get; set; }
        private string Token { get; set; }
        public Uri RequestUri { get; set; }
        public HTTPVerb Verb { get; set; }


        public HttpWebResponse ExecuteRequest()
        {
            HttpWebRequest request = PrepareRequest();
            return (HttpWebResponse)request.GetResponse();
        }

        /// <summary>
        /// Prepares the request. It is not nessisary to call this method unless additional configuration is required.
        /// </summary>
        /// <returns>A <see cref="HttpWebRequest"/> object fully configured and ready for execution.</returns>
        public HttpWebRequest PrepareRequest()
        {
            string contentType = string.Empty;
            SetupToken();
            AddQueryStringParametersToUri();

            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(this.RequestUri);
            request.UseDefaultCredentials = true;

            request.Method = this.Verb.ToString();

            request.ContentLength = 0;

            request.UserAgent = (string.IsNullOrEmpty(UserAgent)) ? string.Format(CultureInfo.InvariantCulture, "CGITNCDC/{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version) : UserAgent;

            return request;
        }

        private void SetupToken()
        {
            this.Parameters.Add("token", this.Token);
        }

        private void AddQueryStringParametersToUri()
        {
            StringBuilder requestParametersBuilder = new StringBuilder(this.RequestUri.AbsoluteUri);
            requestParametersBuilder.Append(this.RequestUri.Query.Length == 0 ? "?" : "&");

            foreach (KeyValuePair<string, object> item in Parameters)
            {
                if (item.Value is string)
                    requestParametersBuilder.AppendFormat("{0}={1}&", item.Key, UrlEncode((string)item.Value));
            }

            if (requestParametersBuilder.Length == 0)
                return;

            requestParametersBuilder.Remove(requestParametersBuilder.Length - 1, 1);

            this.RequestUri = new Uri(requestParametersBuilder.ToString());
        }

        public static string UrlEncode(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            value = Uri.EscapeDataString(value);

            // UrlEncode escapes with lowercase characters (e.g. %2f) but oAuth needs %2F
            value = Regex.Replace(value, "(%[0-9a-f][0-9a-f])", c => c.Value.ToUpper());

            // these characters are not escaped by UrlEncode() but needed to be escaped
            value = value
                .Replace("(", "%28")
                .Replace(")", "%29")
                .Replace("$", "%24")
                .Replace("!", "%21")
                .Replace("*", "%2A")
                .Replace("'", "%27");

            // these characters are escaped by UrlEncode() but will fail if unescaped!
            value = value.Replace("%7E", "~");

            return value;
        }
    }
}
