using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NCDCWebService.Models.Serialization
{
    public static class NCDCSerialization<T>
        where T : INCDCObject
    {
        /// <summary>
        /// The JavascriptConversionDelegate. The delegate is invokes when using the JavaScriptSerializer to manually construct a result object.
        /// </summary>
        /// <param name="value">Contains nested dictionary objects containing deserialized values for manual parsing.</param>
        /// <returns>A strongly typed object representing the deserialized data of type T.
        /// </returns>
        public delegate T DeserializationHandler(JObject value);

        /// <summary>
        /// Deserializes the specified web response.
        /// </summary>
        /// <param name="webResponseData">The web response data.</param>
        /// <param name="deserializationHandler">The deserialization handler.</param>
        /// <returns>
        /// A strongly typed object representing the deserialized data of type <typeparamref name="T"/>
        /// </returns>
        public static T Deserialize(byte[] webResponseData, DeserializationHandler deserializationHandler)
        {
            T resultObject;

            // Deserialize the results.
            if (deserializationHandler == null)
            {
                resultObject = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(webResponseData, 0, webResponseData.Length));
            }
            else
            {
                resultObject = deserializationHandler((JObject)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(webResponseData, 0, webResponseData.Length)));
            }

            return resultObject;
        }

        /// <summary>
        /// Deserializes the specified web response.
        /// </summary>
        /// <param name="webResponseData">The web response data.</param>
        /// <returns>
        /// A strongly typed object representing the deserialized data of type <typeparamref name="T"/>
        /// </returns>
        public static T Deserialize(byte[] webResponseData)
        {
            return Deserialize(webResponseData, null);
        }
    }
}
