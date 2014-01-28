using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace NCDCWebService.Models.Collections
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [JsonConverter(typeof(NCDCDatasetCollection.Converter))]
    public class NCDCDatasetCollection : NCDCCollection<NCDCDataset>, INCDCObject
    {
        [DataMember, JsonProperty(PropertyName = "@totalCount")]
        public string ItemCount { get; set; }
        [DataMember, JsonProperty(PropertyName = "@pageCount")]
        public string PageCount { get; set; }
        [DataMember, JsonProperty(PropertyName = "@page")]
        public string CurrentPage { get; set; }

        internal static NCDCDatasetCollection Deserialize(JObject value)
        {
            if (value == null || value.First == null || value.First.First == null)
                return null;
            var stringValue = value.First.First.ToString();
            var obj = JsonConvert.DeserializeObject<NCDCDatasetCollection>(stringValue);
            return obj;
        }

        #region Converter
        internal class Converter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(NCDCDatasetCollection);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                NCDCDatasetCollection result = new NCDCDatasetCollection();
                while (reader.Read())
                {
                    try
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            var deserialize = serializer.Deserialize<NCDCDataset>(reader);
                            result.Add(deserialize);
                        }
                        else if (reader.TokenType == JsonToken.PropertyName)
                        {
                            switch (reader.Value.ToString())
                            {
                                case "@totalCount":
                                    reader.Read();
                                    result.ItemCount = reader.Value.ToString();
                                    break;
                                case "@pageCount":
                                    reader.Read();
                                    result.PageCount = reader.Value.ToString();
                                    break;
                                case "@page":
                                    reader.Read();
                                    result.CurrentPage = reader.Value.ToString();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {

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
