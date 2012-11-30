using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NCDCWebService.Models;

namespace NCDCWebService.Converters
{
    public class NCDCLocationLabelsConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        /// <remarks></remarks>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(NCDCLocation);
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
            NCDCLocation result = existingValue as NCDCLocation;
            if (result == null)
                result = new NCDCLocation();

            while (reader.Read())
            {if (reader.TokenType == JsonToken.PropertyName)
                {
                    switch (reader.Value.ToString())
                    {
                        case "type":
                            reader.Read();
                            var valString = reader.Value.ToString();
                            var locationType = new NCDCLocationType { id = valString };
                            result.LocationType = locationType;
                            break;
                        case "id":
                            reader.Read();
                            valString = reader.Value.ToString();
                            result.id = valString;
                            break;
                        case "displayName":
                            reader.Read();
                            valString = reader.Value.ToString();
                            result.DisplayName = valString;
                            break;
                    }
                }
            }

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
