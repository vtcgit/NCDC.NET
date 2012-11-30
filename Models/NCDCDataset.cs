using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models.Interfaces;
using NCDCWebService.Models.Collections;
using NCDCWebService.Options;
using NCDCWebService.Command;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using NCDCWebService.Converters;
using Newtonsoft.Json.Linq;

namespace NCDCWebService.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    [DataContract]
    public class NCDCDataset : INCDCObject
    {
        #region Properties
        [DataMember, JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [DataMember, JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [DataMember, JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [DataMember, JsonProperty(PropertyName = "minDate"), JsonConverter(typeof(NCDCDateConverter))]
        public DateTime MinimumDate { get; set; }
        [DataMember, JsonProperty(PropertyName = "maxDate"), JsonConverter(typeof(NCDCDateConverter))]
        public DateTime MaximumDate { get; set; }
        [DataMember, JsonProperty(PropertyName = "attributes"), JsonConverter(typeof(NCDCAttributeConverter))]
        public NCDCAttributeCollection Attributes { get; set; }
        #endregion

        #region Commands
        public static NCDCResponse<NCDCDatasetCollection> GetDataSets(string token, NCDCOptions options)
        {
            var command = new ListDatasetsCommand(token, options);
            return NCDCCommand<NCDCDatasetCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataset> GetDatasetInformation(string datasetName, string token, NCDCOptions options)
        {
            var command = new ShowDatasetCommand(datasetName, token, options);
            return NCDCCommand<NCDCDataset>.PerformAction(command);
        }
        #endregion

        #region Deserialization
        internal static NCDCDataset Deserialize(JObject value)
        {
            if (value == null || value.First == null || value.First.First["dataSet"] == null && value.First.First["dataSet"].Count() == 0)
                return null;
            var val = value.First.First["dataSet"];
            if (val is JArray && (val as JArray).Count > 0)
                val = val.First;
            return JsonConvert.DeserializeObject<NCDCDataset>(val.ToString());
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("({0}) {1}", Name, Description);
        }
        #endregion
    }
}
