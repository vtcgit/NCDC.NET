using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using NCDCWebService.Models.Interfaces;
using Newtonsoft.Json.Linq;
using NCDCWebService.Command;
using NCDCWebService.Options;
using NCDCWebService.Models.Collections;

namespace NCDCWebService.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    [DataContract]
    public class NCDCLocationType : INCDCObject
    {
        #region Properties
        [DataMember, JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [DataMember, JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        #endregion

        #region Commands
        public static NCDCResponse<NCDCLocationTypeCollection> GetLocationTypes(string datasetName, string token, NCDCOptions options)
        {
            var command = new ListLocationTypesCommand(datasetName, token, options);
            return NCDCCommand<NCDCLocationTypeCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCLocationType> GetLoctionTypeInformation(string datasetName, string locationTypeName, string token, NCDCOptions options)
        {
            var command = new ShowLocationTypeCommand(datasetName, locationTypeName, token, options);
            return NCDCCommand<NCDCLocationType>.PerformAction(command);
        }
        #endregion

        #region Deserialization
        internal static NCDCLocationType Deserialize(JObject value)
        {
            if (value == null || value.First == null || value.First.First["locationType"] == null && value.First.First["locationType"].Count() == 0)
                return null;
            var val = value.First.First["locationType"];
            if (val is JArray && (val as JArray).Count > 0)
                val = val.First;
            return JsonConvert.DeserializeObject<NCDCLocationType>(val.ToString());
        }
        #endregion
    }
}
