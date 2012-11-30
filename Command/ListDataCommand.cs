using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models;
using NCDCWebService.Options;
using NCDCWebService.Models.Collections;
using System.Globalization;

namespace NCDCWebService.Command
{
    class ListDataCommand : NCDCCommand<NCDCDataCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDataCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeName">Name of the location type.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="stationId">The station id.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListDataCommand(string datasetName, string locationTypeName, string locationId, string stationId, string dataTypeId, string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/locationtypes/{1}/locations/{2}/stations/{3}/datatypes/{4}/data.json", datasetName, locationTypeName, locationId, stationId, dataTypeId),
            token, options)
        {
            this.DataSetName = datasetName;
            this.LocationId = locationId;
            this.LocationTypeId = locationTypeName;
            this.StationId = stationId;
            this.DataTypeId = dataTypeId;
            this.DeserializationHandler = NCDCDataCollection.Deserialize;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDataCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListDataCommand(string datasetName, string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/data.json", datasetName),
            token, options)
        {
            this.DataSetName = datasetName;
            this.DeserializationHandler = NCDCDataCollection.Deserialize;
        }

        // all other types must go here
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDataCommand"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListDataCommand(string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            "",
            token, options)
        {
            this.DeserializationHandler = NCDCDataCollection.Deserialize;
        }

        /// <summary>
        /// Builds the dataset data type URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <remarks></remarks>
        public void BuildDatasetDataTypeUri(string datasetName, string dataTypeId)
        {
            this.DataSetName = datasetName;
            this.DataTypeId = dataTypeId;
            base.SetCommandUri(string.Format("datasets/{0}/datatypes/{1}/data.json", datasetName, dataTypeId));
        }

        #region Location Searches
        /// <summary>
        /// Builds the dataset location URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationId">The location id.</param>
        /// <remarks></remarks>
        public void BuildDatasetLocationUri(string datasetName, string locationId)
        {
            this.DataSetName = datasetName;
            this.LocationId = locationId;
            base.SetCommandUri(string.Format("datasets/{0}/locations/{1}/data.json", datasetName, locationId));
        }
        /// <summary>
        /// Builds the dataset location data type URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <remarks></remarks>
        public void BuildDatasetLocationDataTypeUri(string datasetName, string locationId, string dataTypeId)
        {
            this.DataSetName = datasetName;
            this.LocationId = locationId;
            this.DataTypeId = dataTypeId;
            base.SetCommandUri(string.Format("datasets/{0}/locations/{1}/datatypes/{2}/data.json", datasetName, locationId, dataTypeId));
        }
        /// <summary>
        /// Builds the dataset location station URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="stationId">The station id.</param>
        /// <remarks></remarks>
        public void BuildDatasetLocationStationUri(string datasetName, string locationId, string stationId)
        {
            this.DataSetName = datasetName;
            this.LocationId = locationId;
            this.StationId = stationId;
            base.SetCommandUri(string.Format("datasets/{0}/locations/{1}/stations/{2}/data.json", datasetName, locationId, stationId));
        }
        /// <summary>
        /// Builds the dataset location station datatype URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="stationId">The station id.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <remarks></remarks>
        public void BuildDatasetLocationStationDatatypeUri(string datasetName, string locationId, string stationId, string dataTypeId)
        {
            this.DataSetName = datasetName;
            this.LocationId = locationId;
            this.StationId = stationId;
            this.DataTypeId = dataTypeId;
            base.SetCommandUri(string.Format("datasets/{0}/locations/{1}/stations/{2}/datatypes/{3}/data.json", datasetName, locationId, stationId, dataTypeId));
        }
        #endregion

        #region Station Searches
        /// <summary>
        /// Builds the dataset station URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="stationId">The station id.</param>
        /// <remarks></remarks>
        public void BuildDatasetStationUri(string datasetName, string stationId)
        {
            this.DataSetName = datasetName;
            this.StationId = stationId;
            base.SetCommandUri(string.Format("datasets/{0}/stations/{1}/data.json", datasetName, stationId));
        }
        /// <summary>
        /// Builds the dataset station data type URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="stationId">The station id.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <remarks></remarks>
        public void BuildDatasetStationDataTypeUri(string datasetName, string stationId, string dataTypeId)
        {
            this.DataSetName = datasetName;
            this.StationId = stationId;
            this.DataTypeId = dataTypeId;
            base.SetCommandUri(string.Format("datasets/{0}/stations/{1}/datatypes/{2}/data.json", datasetName, stationId, dataTypeId));
        }
        #endregion

        #region Location Type Searches
        /// <summary>
        /// Builds the dataset location type URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeId">The location type id.</param>
        /// <remarks></remarks>
        public void BuildDatasetLocationTypeUri(string datasetName, string locationTypeId)
        {
            this.DataSetName = datasetName;
            base.SetCommandUri(string.Format("datasets/{0}/locationtypes/{1}/data", datasetName, locationTypeId));
        }
        /// <summary>
        /// Builds the dataset location type data types URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeId">The location type id.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <remarks></remarks>
        public void BuildDatasetLocationTypeDataTypesUri(string datasetName, string locationTypeId, string dataTypeId)
        {
            this.DataSetName = datasetName;
            this.DataTypeId = dataTypeId;
            base.SetCommandUri(string.Format("datasets/{0}/locationtypes/{1}/datatypes/{2}/data", datasetName, locationTypeId, dataTypeId));
        }
        /// <summary>
        /// Builds the dataset location type location URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeId">The location type id.</param>
        /// <param name="locationId">The location id.</param>
        /// <remarks></remarks>
        public void BuildDatasetLocationTypeLocationUri(string datasetName, string locationTypeId, string locationId)
        {
            this.DataSetName = datasetName;
            this.LocationId = locationId;
            base.SetCommandUri(string.Format("datasets/{0}/locationtypes/{1}/locations/{2}/data", datasetName, locationTypeId, locationId));
        }
        /// <summary>
        /// Builds the dataset location type location data type URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeId">The location type id.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <remarks></remarks>
        public void BuildDatasetLocationTypeLocationDataTypeUri(string datasetName, string locationTypeId, string locationId, string dataTypeId)
        {
            this.DataSetName = datasetName;
            this.LocationId = locationId;
            this.DataTypeId = dataTypeId;
            base.SetCommandUri(string.Format("datasets/{0}/locationtypes/{1}/locations/{2}/datatypes/{3}/data", datasetName, locationTypeId, locationId, dataTypeId));
        }
        /// <summary>
        /// Builds the dataset location type location station URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeId">The location type id.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="stationId">The station id.</param>
        /// <remarks></remarks>
        public void BuildDatasetLocationTypeLocationStationUri(string datasetName, string locationTypeId, string locationId, string stationId)
        {
            this.DataSetName = datasetName;
            this.LocationId = locationId;
            this.StationId = stationId;
            base.SetCommandUri(string.Format("datasets/{0}/locationtypes/{1}/locations/{2}/stations/{3}/data", datasetName, locationTypeId, locationId, stationId));
        }
        #endregion


        /// <summary>
        /// Inits this instance.
        /// </summary>
        /// <remarks></remarks>
        public override void Init()
        {
            NCDCOptions options = this.NCDCOptions as NCDCOptions;

            if (options == null)
            {
                this.RequestParameters.Add("page", "1");

                return;
            }

            this.RequestParameters.Add("page", options.Page > 0 ? options.Page.ToString(CultureInfo.InvariantCulture) : "1");
        }
    }
}
