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
    public class NCDCData : INCDCObject
    {
        #region Properties
        [DataMember, JsonProperty(PropertyName = "date"), JsonConverter(typeof(NCDCDateConverter))]
        public DateTime Date { get; set; }
        [DataMember, JsonProperty(PropertyName = "dataType"), JsonConverter(typeof(NCDCDataTypeConverter))]
        public NCDCDataType DataType { get; set; }
        [DataMember, JsonProperty(IsReference = true, PropertyName = "station"), JsonConverter(typeof(NCDCStationConverter))]
        public NCDCStation Station { get; set; }
        [DataMember, JsonProperty(PropertyName = "value")]
        public int Value { get; set; }
        [DataMember, JsonProperty(PropertyName = "attributes"), JsonConverter(typeof(NCDCAttributeConverter))]
        public NCDCAttributeCollection Attributes { get; set; }
        #endregion

        #region Commands

        public static NCDCResponse<NCDCDataCollection> GetDataForLocationTypeLocationStationAndDataType(string datasetName,
            string locationTypeId, string locationId, string stationId, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(datasetName, locationTypeId, locationId, stationId, dataTypeId, token, options);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForDataset(string datasetName, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(datasetName, token, options);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForDataType(string datasetName, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(token, options);
            command.BuildDatasetDataTypeUri(datasetName, dataTypeId);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationAndDataType(string datasetName, string locationId, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(token, options);
            command.BuildDatasetLocationDataTypeUri(datasetName, locationId, dataTypeId);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationStationAndDataType(string datasetName, 
            string locationId, string stationId, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(token, options);
            command.BuildDatasetLocationStationDatatypeUri(datasetName, locationId, stationId, dataTypeId);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationAndStation(string datasetName, string locationId, string stationId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(token, options);
            command.BuildDatasetLocationStationUri(datasetName, locationId, stationId);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationTypeAndDataType(string datasetName,
            string locationTypeId, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(token, options);
            command.BuildDatasetLocationTypeDataTypesUri(datasetName, locationTypeId, dataTypeId);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationTypeLocationAndDataType(string datasetName,
            string locationTypeId, string locationId, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(token, options);
            command.BuildDatasetLocationTypeLocationDataTypeUri(datasetName, locationTypeId, locationId, dataTypeId);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationTypeLocationAndStation(string datasetName,
            string locationTypeId, string locationId, string stationId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(token, options);
            command.BuildDatasetLocationTypeLocationStationUri(datasetName, locationTypeId, locationId, stationId);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationTypeAndLocation(string datasetName,
            string locationTypeId, string locationId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(token, options);
            command.BuildDatasetLocationTypeLocationUri(datasetName, locationTypeId, locationId);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationType(string datasetName, string locationTypeId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(token, options);
            command.BuildDatasetLocationTypeUri(datasetName, locationTypeId);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForLocation(string datasetName,
            string locationTypeName, string locationId, string stationId, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(token, options);
            command.BuildDatasetLocationUri(datasetName, locationId);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForStationAndDataType(string datasetName, string stationId, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(token, options);
            command.BuildDatasetStationDataTypeUri(datasetName, stationId, dataTypeId);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataCollection> GetDataForStation(string datasetName, string stationId, string token, NCDCOptions options)
        {
            var command = new ListDataCommand(token, options);
            command.BuildDatasetStationUri(datasetName, stationId);
            return NCDCCommand<NCDCDataCollection>.PerformAction(command);
        }
        #endregion

        #region Deserialization
        internal static NCDCData Deserialize(JObject value)
        {
            if (value == null || value.First == null || value.First.First["data"] == null && value.First.First["data"].Count() == 0)
                return null;
            var val = value.First.First["data"];
            if (val is JArray && (val as JArray).Count > 0)
                val = val.First;
            return JsonConvert.DeserializeObject<NCDCData>(val.ToString());
        }
        #endregion
    }
}
