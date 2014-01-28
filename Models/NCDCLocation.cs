using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using NCDCWebService.Models.Interfaces;
using NCDCWebService.Converters;
using Newtonsoft.Json.Linq;
using NCDCWebService.Models.Collections;
using NCDCWebService.Command;
using NCDCWebService.Options;

namespace NCDCWebService.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    [DataContract]
    public class NCDCLocation : INCDCObject
    {
        #region Properties
        [DataMember, JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [DataMember, JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }
        [DataMember, JsonProperty(PropertyName = "minDate"), JsonConverter(typeof(NCDCDateConverter))]
        public DateTime MinimumDate { get; set; }
        [DataMember, JsonProperty(PropertyName = "maxDate"), JsonConverter(typeof(NCDCDateConverter))]
        public DateTime MaximumDate { get; set; }
        [DataMember, JsonProperty(PropertyName = "stationCount")]
        public int? StationCount { get; set; }
        [DataMember, JsonProperty(PropertyName = "coverage")]
        public decimal? Coverage { get; set; }
        [DataMember, JsonProperty(PropertyName = "locationType")]
        public NCDCLocationType LocationType { get; set; }
        #endregion

        #region Commands
        public static NCDCResponse<NCDCLocationCollection> GetLocations(string datasetName, string token = null, NCDCOptions options = null)
        {
            token = token ?? NCDCUtilities.GetUnlockedToken();
            var command = new ListLocationsCommand(datasetName, token, options);
            return NCDCCommand<NCDCLocationCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCLocationCollection> GetLocations(string datasetName, string locationTypeName, string token = null, NCDCOptions options = null)
        {
            token = token ?? NCDCUtilities.GetUnlockedToken();
            var command = new ListLocationsCommand(datasetName, locationTypeName, token, options);
            return NCDCCommand<NCDCLocationCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCLocation> GetLocationInformation(string datasetName, string locationTypeName, string locationId, string token = null, NCDCOptions options = null)
        {
            token = token ?? NCDCUtilities.GetUnlockedToken();
            var command = new ShowLocationCommand(datasetName, locationTypeName, locationId, token, options);
            return NCDCCommand<NCDCLocation>.PerformAction(command);
        }
        public static NCDCResponse<NCDCLocation> GetLocationInformation(string datasetName, string locationId, string token = null, NCDCOptions options = null)
        {
            token = token ?? NCDCUtilities.GetUnlockedToken();
            var command = new ShowLocationCommand(datasetName, locationId, token, options);
            return NCDCCommand<NCDCLocation>.PerformAction(command);
        }
        #endregion

        #region Deserialization
        internal static NCDCLocation Deserialize(JObject value)
        {
            if (value == null || value.First == null || value.First.First["location"] == null && value.First.First["location"].Count() == 0)
                return null;
            var val = value.First.First["location"];
            if (val is JArray && (val as JArray).Count > 0)
                val = val.First;
            return JsonConvert.DeserializeObject<NCDCLocation>(val.ToString());
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("({0}) {1}", id, DisplayName);
        }
        #endregion

    }
}
