using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCDCWebService
{
    public enum HTTPVerb
    {
        /// <summary>
        /// The HTTP GET method is used to retrieve data.
        /// </summary>
        GET,

        /// <summary>
        /// The HTTP POST method is used to transmit data.
        /// </summary>
        POST,

        /// <summary>
        /// The HTTP DELETE method is used to indicate that a resource should be deleted.
        /// </summary>
        DELETE
    }

    public enum RequestResult
    {
        Success,
        FileNotFound,
        BadRequest,
        Unauthorized,
        NotAcceptable,
        RateLimited,
        ConnectionFailure,
        Unknown
    }
}
