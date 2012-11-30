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
    public class NCDCDataType : INCDCObject
    {
        #region Properties
        [DataMember, JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [DataMember, JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        #endregion

        #region Commands
        public static NCDCResponse<NCDCDataTypeCollection> GetDataTypesForLocationTypeLocationAndStation(string datasetName, string locationTypeName, string locationId, string stationId, string token, NCDCOptions options)
        {
            var command = new ListDataTypesCommand(datasetName, locationTypeName, locationId, stationId, token, options);
            return NCDCCommand<NCDCDataTypeCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataTypeCollection> GetDataTypesForDataset(string datasetName, string token, NCDCOptions options)
        {
            var command = new ListDataTypesCommand(datasetName, token, options);
            return NCDCCommand<NCDCDataTypeCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataTypeCollection> GetDataTypesForLocationAndStation(string datasetName, string locationId, string stationId, string token, NCDCOptions options)
        {
            var command = new ListDataTypesCommand(token, options);
            command.BuildDatasetsLocationsStationsUri(datasetName, locationId, stationId);
            return NCDCCommand<NCDCDataTypeCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataTypeCollection> GetDataTypesForLocation(string datasetName, string locationId, string token, NCDCOptions options)
        {
            var command = new ListDataTypesCommand(token, options);
            command.BuildDatasetsLocationsUri(datasetName, locationId);
            return NCDCCommand<NCDCDataTypeCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataTypeCollection> GetDataTypesForLocationTypeAndLocation(string datasetName, string locationTypeName, string locationId, string token, NCDCOptions options)
        {
            var command = new ListDataTypesCommand(token, options);
            command.BuildDatasetsLocationTypesLocationsUri(datasetName, locationTypeName, locationId);
            return NCDCCommand<NCDCDataTypeCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataTypeCollection> GetDataTypesForLocationType(string datasetName, string locationTypeName, string token, NCDCOptions options)
        {
            var command = new ListDataTypesCommand(token, options);
            command.BuildDatasetsLocationTypesUri(datasetName, locationTypeName);
            return NCDCCommand<NCDCDataTypeCollection>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataTypeCollection> GetDataTypesForStation(string datasetName, string stationId, string token, NCDCOptions options)
        {
            var command = new ListDataTypesCommand(token, options);
            command.BuildDatasetsStationsUri(datasetName, stationId);
            return NCDCCommand<NCDCDataTypeCollection>.PerformAction(command);
        }


        public static NCDCResponse<NCDCDataType> GetDataTypeInformationForLocationTypeLocationAndStation(string datasetName, string locationTypeName, string locationId, string stationId, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ShowDataTypesCommand(datasetName, locationTypeName, locationId, stationId, dataTypeId, token, options);
            return NCDCCommand<NCDCDataType>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataType> GetDataTypeInformationForDataset(string datasetName, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ShowDataTypesCommand(datasetName, dataTypeId, token, options);
            return NCDCCommand<NCDCDataType>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataType> GetDataTypeInformationForLocationAndStation(string datasetName, string locationId, string stationId, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ShowDataTypesCommand(token, options);
            command.BuildDatasetsLocationsStationsUri(datasetName, locationId, stationId, dataTypeId);
            return NCDCCommand<NCDCDataType>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataType> GetDataTypeInformationForLocation(string datasetName, string locationId, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ShowDataTypesCommand(token, options);
            command.BuildDatasetsLocationsUri(datasetName, locationId, dataTypeId);
            return NCDCCommand<NCDCDataType>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataType> GetDataTypeInformationForLocationTypeAndLocation(string datasetName, string locationTypeName, string locationId, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ShowDataTypesCommand(token, options);
            command.BuildDatasetsLocationTypesLocationsUri(datasetName, locationTypeName, locationId, dataTypeId);
            return NCDCCommand<NCDCDataType>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataType> GetDataTypeInformationForLocationType(string datasetName, string locationTypeName, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ShowDataTypesCommand(token, options);
            command.BuildDatasetsLocationTypesUri(datasetName, locationTypeName, dataTypeId);
            return NCDCCommand<NCDCDataType>.PerformAction(command);
        }
        public static NCDCResponse<NCDCDataType> GetDataTypeInformationForStation(string datasetName, string stationId, string dataTypeId, string token, NCDCOptions options)
        {
            var command = new ShowDataTypesCommand(token, options);
            command.BuildDatasetsStationsUri(datasetName, stationId, dataTypeId);
            return NCDCCommand<NCDCDataType>.PerformAction(command);
        }
        #endregion

        #region Deserialization
        internal static NCDCDataType Deserialize(JObject value)
        {
            if (value == null || value.First == null || value.First.First["dataType"] == null && value.First.First["dataType"].Count() == 0)
                return null;
            var val = value.First.First["dataType"];
            if (val is JArray && (val as JArray).Count > 0)
                val = val.First;
            return JsonConvert.DeserializeObject<NCDCDataType>(val.ToString());
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("({0}) {1}", id, Description);
        }
        #endregion
    }
}
