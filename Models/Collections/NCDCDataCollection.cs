using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models.Interfaces;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace NCDCWebService.Models.Collections
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [JsonConverter(typeof(NCDCDataCollection.Converter))]
    public class NCDCDataCollection : NCDCCollection<NCDCData>, INCDCObject
    {
        [DataMember, JsonProperty(PropertyName = "@totalCount")]
        public int ItemCount { get; set; }
        [DataMember, JsonProperty(PropertyName = "@pageCount")]
        public int PageCount { get; set; }
        [DataMember, JsonProperty(PropertyName = "@page")]
        public int CurrentPage { get; set; }

        internal static NCDCDataCollection Deserialize(JObject value)
        {
            if (value == null || value.First == null || value.First.First == null)
                return null;

            return JsonConvert.DeserializeObject<NCDCDataCollection>(value.First.First.ToString());
        }

        #region Converter
        internal class Converter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(NCDCDataCollection);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                NCDCDataCollection result = new NCDCDataCollection();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        var dataObject = serializer.Deserialize<NCDCData>(reader);
                        result.Add(dataObject);
                    }
                    else if (reader.TokenType == JsonToken.PropertyName)
                    {
                        int num = 0;
                        switch (reader.Value.ToString())
                        {
                            case "@totalCount":
                                reader.Read();
                                int.TryParse(reader.Value.ToString(), out num);
                                result.ItemCount = num;
                                break;
                            case "@pageCount":
                                reader.Read();
                                int.TryParse(reader.Value.ToString(), out num);
                                result.PageCount = num;
                                break;
                            case "@page":
                                reader.Read();
                                int.TryParse(reader.Value.ToString(), out num);
                                result.CurrentPage = num;
                                break;
                            default:
                                break;
                        }
                    }
                }

                return result;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
