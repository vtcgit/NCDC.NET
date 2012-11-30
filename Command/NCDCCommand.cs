using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models.Interfaces;
using NCDCWebService.Models;
using System.Globalization;
using NCDCWebService.Utilities;
using System.Net;
using NCDCWebService.Models.Serialization;
using NCDCWebService.Options;
using System.Threading;

namespace NCDCWebService.Command
{
    /// <summary>
    /// NCDC Commands
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>Some Commands are borrowed from Twitterizer2</remarks>
    [Serializable]
    public abstract class NCDCCommand<T> : ICommand<T>
        where T : INCDCObject
    {
        /// <summary>
        /// Gets or sets the name of the data set.
        /// </summary>
        /// <value>The name of the data set.</value>
        /// <remarks></remarks>
        public string DataSetName { get; set; }
        /// <summary>
        /// Gets or sets the location type id.
        /// </summary>
        /// <value>The location type id.</value>
        /// <remarks></remarks>
        public string LocationTypeId { get; set; }
        /// <summary>
        /// Gets or sets the location id.
        /// </summary>
        /// <value>The location id.</value>
        /// <remarks></remarks>
        public string LocationId { get; set; }
        /// <summary>
        /// Gets or sets the station id.
        /// </summary>
        /// <value>The station id.</value>
        /// <remarks></remarks>
        public string StationId { get; set; }
        /// <summary>
        /// Gets or sets the data type id.
        /// </summary>
        /// <value>The data type id.</value>
        /// <remarks></remarks>
        public string DataTypeId { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="NCDCCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="endPoint">The end point.</param>
        /// <param name="token">The token.</param>
        /// <param name="optionalProperties">The optional properties.</param>
        /// <remarks></remarks>
        protected NCDCCommand(HTTPVerb method, string endPoint, string token, NCDCOptions optionalProperties)
        {
            this.RequestParameters = new Dictionary<string, object>();
            this.Verb = method;
            this.Token = token;
            this.NCDCOptions = optionalProperties ?? new NCDCOptions();
            NCDCUtilities.CurrentToken = this.Token;
            this.SetCommandUri(endPoint);
        }

        /// <summary>
        /// Gets or sets the API method URI.
        /// </summary>
        /// <value>The URI for the API method.</value>
        /// <remarks></remarks>
        private Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        private HTTPVerb Verb { get; set; }

        /// <summary>
        /// Gets or sets the request parameters.
        /// </summary>
        /// <value>The request parameters.</value>
        public Dictionary<string, object> RequestParameters { get; set; }

        /// <summary>
        /// Gets or sets the serialization delegate.
        /// </summary>
        /// <value>The serialization delegate.</value>
        public NCDCSerialization<T>.DeserializationHandler DeserializationHandler { get; set; }

        /// <summary>
        /// Gets the request tokens.
        /// </summary>
        /// <value>The request tokens.</value>
        internal string Token { get; private set; }
        /// <summary>
        /// Gets or sets the NCDC options.
        /// </summary>
        /// <value>The NCDC options.</value>
        /// <remarks></remarks>
        public NCDCOptions NCDCOptions { get; set; }

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NCDCCommand&lt;T&gt;"/> is multipart.
        /// </summary>
        /// <value><c>true</c> if multipart; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        protected bool Multipart { get; set; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns>The results of the command.</returns>
        /// <remarks></remarks>
        public NCDCResponse<T> ExecuteCommand()
        {
            this.Token = NCDCUtilities.GetUnlockedToken();
            lock (Token)
            {
                NCDCResponse<T> ncdcResponse = new NCDCResponse<T>();

                ncdcResponse.Command = this;
                if (true) // use SSL
                {
                    this.Uri = new Uri(this.Uri.AbsoluteUri.Replace("http://", "https://"));
                }

                // check if token exists
                if (string.IsNullOrEmpty(this.Token))
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Token is required for the \"{0}\" command.", this.GetType()));

                // Prepare the query parameters
                Dictionary<string, object> queryParameters = new Dictionary<string, object>();
                foreach (KeyValuePair<string, object> item in this.RequestParameters)
                {
                    queryParameters.Add(item.Key, item.Value);
                }

                // Declare the variable to be returned
                ncdcResponse.ResponseObject = default(T);
                ncdcResponse.RequestUrl = this.Uri.AbsoluteUri;
                byte[] responseData;

                try
                {
                    NCDCWebRequest requestBuilder = new NCDCWebRequest(this.Uri, this.Verb, this.Token);

                    foreach (var item in queryParameters)
                    {
                        requestBuilder.Parameters.Add(item.Key, item.Value);
                    }

                    HttpWebResponse response = requestBuilder.ExecuteRequest();

                    if (response == null)
                    {
                        ncdcResponse.Result = RequestResult.Unknown;
                        return ncdcResponse;
                    }

                    responseData = NCDCUtilities.ReadStream(response.GetResponseStream());
                    ncdcResponse.Content = Encoding.UTF8.GetString(responseData, 0, responseData.Length);

                    ncdcResponse.RequestUrl = requestBuilder.RequestUri.AbsoluteUri;

                    SetStatusCode(ncdcResponse, response.StatusCode);
                }
                catch (WebException wex)
                {
                    if (new[]
                        {
                            WebExceptionStatus.Timeout, 
                            WebExceptionStatus.ConnectionClosed,
                            WebExceptionStatus.ConnectFailure
                        }.Contains(wex.Status))
                    {
                        ncdcResponse.Result = RequestResult.ConnectionFailure;
                        ncdcResponse.ErrorMessage = wex.Message;
                        return ncdcResponse;
                    }

                    // The exception response should always be an HttpWebResponse, but we check for good measure.
                    HttpWebResponse exceptionResponse = wex.Response as HttpWebResponse;

                    if (exceptionResponse == null)
                    {
                        throw;
                    }

                    responseData = NCDCUtilities.ReadStream(exceptionResponse.GetResponseStream());
                    ncdcResponse.Content = Encoding.UTF8.GetString(responseData, 0, responseData.Length);

                    SetStatusCode(ncdcResponse, exceptionResponse.StatusCode);

                    if (wex.Status == WebExceptionStatus.UnknownError)
                        throw;
                    return ncdcResponse;
                }

                try
                {
                    ncdcResponse.ResponseObject = NCDCSerialization<T>.Deserialize(responseData, this.DeserializationHandler);
                }
                catch (Newtonsoft.Json.JsonReaderException)
                {
                    ncdcResponse.ErrorMessage = "Unable to parse JSON";
                    ncdcResponse.Result = RequestResult.Unknown;
                    return ncdcResponse;
                }
                catch (Newtonsoft.Json.JsonSerializationException)
                {
                    ncdcResponse.ErrorMessage = "Unable to parse JSON";
                    ncdcResponse.Result = RequestResult.Unknown;
                    return ncdcResponse;
                }

                // Pass the current oauth tokens into the new object, so method calls from there will keep the authentication.
                ncdcResponse.Token = this.Token;

                //ncdcResponse.UpdateContent();
                return ncdcResponse;
            }
        }

        private static void SetStatusCode(NCDCResponse<T> ncdcResponse, HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    ncdcResponse.Result = RequestResult.Success;
                    break;

                case HttpStatusCode.BadRequest:
                    ncdcResponse.Result = RequestResult.BadRequest;
                    break;

                case (HttpStatusCode)420: //Rate Limited from Search/Trends API
                    ncdcResponse.Result = RequestResult.RateLimited;
                    break;

                case HttpStatusCode.Unauthorized:
                    ncdcResponse.Result = RequestResult.Unauthorized;
                    break;

                case HttpStatusCode.NotFound:
                    ncdcResponse.Result = RequestResult.FileNotFound;
                    break;

                case HttpStatusCode.Forbidden:
                    ncdcResponse.Result = RequestResult.Unauthorized;
                    break;

                default:
                    ncdcResponse.Result = RequestResult.Unknown;
                    break;
            }
        }

        /// <summary>
        /// Sets the command URI.
        /// </summary>
        /// <param name="endPoint">The end point.</param>
        protected void SetCommandUri(string endPoint)
        {
            if (endPoint.StartsWith("/"))
                throw new ArgumentException("The API endpoint cannot begin with a forward slash. This will result in 404 errors and headaches.", "endPoint");

            this.Uri = new Uri(string.Concat(this.NCDCOptions.BaseUrl, endPoint));
        }

        public static NCDCResponse<T> PerformAction<T>(ICommand<T> command)
            where T : INCDCObject
        {
            command.Init();

            return command.ExecuteCommand();
        }
    }
}
