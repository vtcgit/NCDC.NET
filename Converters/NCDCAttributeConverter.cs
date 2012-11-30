using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NCDCWebService.Models.Collections;
using NCDCWebService.Models;

namespace NCDCWebService.Converters
{
    public class NCDCAttributeConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        /// <remarks></remarks>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(NCDCAttributeCollection);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        /// <remarks></remarks>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            NCDCAttributeCollection result = existingValue as NCDCAttributeCollection;
            if (result == null)
                result = new NCDCAttributeCollection();

            int startDepth = reader.Depth;
            string entityType = string.Empty;
            try
            {
                while (reader.Read() && reader.Depth >= startDepth)
                {
                    try
                    {
                        if (reader.TokenType == JsonToken.StartObject || reader.TokenType == JsonToken.StartArray)
                        {
                            NCDCAttribute entity = serializer.Deserialize<NCDCAttribute>(reader);
                            if (entity != null)
                            {
                                result.Add(entity);
                                entity = null;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }

            return result;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <remarks></remarks>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
