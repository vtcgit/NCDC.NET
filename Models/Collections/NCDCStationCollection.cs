﻿using System;
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
    [JsonConverter(typeof(NCDCStationCollection.Converter))]
    public class NCDCStationCollection : NCDCCollection<NCDCStation>, INCDCObject
    {
        [DataMember, JsonProperty(PropertyName = "@totalCount")]
        public int ItemCount { get; set; }
        [DataMember, JsonProperty(PropertyName = "@pageCount")]
        public int PageCount { get; set; }
        [DataMember, JsonProperty(PropertyName = "@page")]
        public int CurrentPage { get; set; }
        [DataMember, JsonProperty(PropertyName = "@pageSize")]
        public int PageSize { get; set; }

        internal static NCDCStationCollection Deserialize(JObject value)
        {
            if (value == null || value.First == null || value.First.First == null)
                return null;

            return JsonConvert.DeserializeObject<NCDCStationCollection>(value.First.First.ToString());
        }

        #region Converter
        internal class Converter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(NCDCStationCollection);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                NCDCStationCollection result = new NCDCStationCollection();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        result.Add(serializer.Deserialize<NCDCStation>(reader));
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
                            case "@pageSize":
                                reader.Read();
                                int.TryParse(reader.Value.ToString(), out num);
                                result.PageSize = num;
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
