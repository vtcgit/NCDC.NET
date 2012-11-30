using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using NCDCWebService.Models.Interfaces;

namespace NCDCWebService.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class NCDCAttribute : INCDCObject
    {
        [DataMember, JsonProperty(PropertyName = "name", Order = 1)]
        public string Name { get; set; }
        [DataMember, JsonProperty(PropertyName = "defaultValue", Order = 2)]
        public string DefaultValue { get; set; }
        [DataMember, JsonProperty(PropertyName = "indexNumber", Order=3 )]
        public int? IndexNumber { get; set; }
    }
}
