using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NCDCWebService.Models.Interfaces;
using NCDCWebService.Converters;
using Newtonsoft.Json.Linq;
using NCDCWebService.Options;
using NCDCWebService.Models.Collections;
using NCDCWebService.Command;

namespace NCDCWebService.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    [DataContract]
    public class NCDCStation : INCDCObject
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
        [DataMember, JsonProperty(PropertyName = "latitude")]
        public decimal? Latitude { get; set; }
        [DataMember, JsonProperty(PropertyName = "longitude")]
        public decimal? Longitude { get; set; }
        [DataMember, JsonProperty(PropertyName = "elevation")]
        public decimal? Elevation { get; set; }
        [DataMember, JsonProperty(PropertyName = "coverage")]
        public decimal? Coverage { get; set; }
        [DataMember, JsonProperty(PropertyName = "stationLabels"), JsonConverter(typeof(NCDCLocationLabelsConverter))]
        public NCDCLocation LocationType { get; set; }
        #endregion

        #region Commands
        public static NCDCResponse<NCDCStationCollection> GetStations(string datasetName, string stationTypeName, string locationId, string token, NCDCOptions options)
        {
            var command = new ListStationsCommand(datasetName, stationTypeName, locationId, token, options);
            return NCDCCommand<NCDCStationCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCStationCollection> GetStations(string datasetName, string locationId, string token, NCDCOptions options)
        {
            var command = new ListStationsCommand(datasetName, locationId, token, options);
            return NCDCCommand<NCDCStationCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCStationCollection> GetStations(string datasetName, string token, NCDCOptions options)
        {
            var command = new ListStationsCommand(datasetName, token, options);
            return NCDCCommand<NCDCStationCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCStation> GetStationInformation(string datasetName, string locationTypeName, string locationId, string stationId, string token, NCDCOptions options)
        {
            var command = new ShowStationCommand(datasetName, locationTypeName, locationId, stationId, token, options);
            return NCDCCommand<NCDCStation>.PerformAction(command);
        }
        public static NCDCResponse<NCDCStation> GetStationInformation(string datasetName, string locationId, string stationId, string token, NCDCOptions options)
        {
            var command = new ShowStationCommand(datasetName, locationId, stationId, token, options);
            return NCDCCommand<NCDCStation>.PerformAction(command);
        }
        public static NCDCResponse<NCDCStation> GetStationInformation(string datasetName, string stationId, string token, NCDCOptions options)
        {
            var command = new ShowStationCommand(datasetName, stationId, token, options);
            return NCDCCommand<NCDCStation>.PerformAction(command);
        }
        #endregion

        #region Deserialization
        internal static NCDCStation Deserialize(JObject value)
        {
            if (value == null || value.First == null || value.First.First["station"] == null && value.First.First["station"].Count() == 0)
                return null;
            var val = value.First.First["station"];
            if (val is JArray && (val as JArray).Count > 0)
                val = val.First;
            return JsonConvert.DeserializeObject<NCDCStation>(val.ToString());
        }
        #endregion
    }
}
