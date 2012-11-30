using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using NCDCWebService.Models.Interfaces;
using Newtonsoft.Json.Linq;

namespace NCDCWebService.Models.Collections
{

    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [JsonConverter(typeof(NCDCLocationTypeCollection.Converter))]
    public class NCDCLocationTypeCollection : NCDCCollection<NCDCLocationType>, INCDCObject
    {
        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>The current page.</value>
        /// <remarks></remarks>
        [DataMember, JsonProperty(PropertyName = "@page")]
        public string CurrentPage { get; set; }

        /// <summary>
        /// Deserializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        internal static NCDCLocationTypeCollection Deserialize(JObject value)
        {
            if (value == null || value.First == null || value.First.First == null)
                return null;

            return JsonConvert.DeserializeObject<NCDCLocationTypeCollection>(value.First.First.ToString());
        }

        #region Converter
        internal class Converter : JsonConverter
        {
            /// <summary>
            /// Determines whether this instance can convert the specified object type.
            /// </summary>
            /// <param name="objectType">Type of the object.</param>
            /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
            /// <remarks></remarks>
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(NCDCLocationTypeCollection);
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
                NCDCLocationTypeCollection result = new NCDCLocationTypeCollection();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        result.Add(serializer.Deserialize<NCDCLocationType>(reader));
                    }
                    else if (reader.TokenType == JsonToken.PropertyName)
                    {
                        switch (reader.Value.ToString())
                        {
                            case "@page":
                                reader.Read();
                                result.CurrentPage = reader.Value.ToString();
                                break;
                            default:
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

        #endregion
    }
}
