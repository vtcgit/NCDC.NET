using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models;
using NCDCWebService.Options;

namespace NCDCWebService.Command
{
    class ShowDataTypesCommand : NCDCCommand<NCDCDataType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowDataTypesCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeName">Name of the location type.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="stationId">The station id.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ShowDataTypesCommand(string datasetName, string locationTypeName, string locationId, string stationId, string dataTypeId, string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/locationtypes/{1}/locations/{2}/stations/{3}/datatypes/{4}.json", datasetName, locationTypeName, locationId, stationId, dataTypeId),
            token, options)
        {
            this.DeserializationHandler = NCDCDataType.Deserialize;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowDataTypesCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ShowDataTypesCommand(string datasetName, string dataTypeId, string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/datatypes/{1}.json", datasetName, dataTypeId),
            token, options)
        {
            this.DeserializationHandler = NCDCDataType.Deserialize;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ShowDataTypesCommand"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ShowDataTypesCommand(string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            "",
            token, options)
        {
            this.DeserializationHandler = NCDCDataType.Deserialize;
        }

        /// <summary>
        /// Builds the datasets location types URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeName">Name of the location type.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <remarks></remarks>
        public void BuildDatasetsLocationTypesUri(string datasetName, string locationTypeName, string dataTypeId)
        {
            base.SetCommandUri(string.Format("datasets/{0}/locationtypes/{1}/datatypes/{2}.json", datasetName, locationTypeName, dataTypeId));
        }
        /// <summary>
        /// Builds the datasets locations URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <remarks></remarks>
        public void BuildDatasetsLocationsUri(string datasetName, string locationId, string dataTypeId)
        {
            base.SetCommandUri(string.Format("datasets/{0}/locations/{1}/datatypes/{2}.json", datasetName, locationId, dataTypeId));
        }
        /// <summary>
        /// Builds the datasets stations URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="stationId">The station id.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <remarks></remarks>
        public void BuildDatasetsStationsUri(string datasetName, string stationId, string dataTypeId)
        {
            base.SetCommandUri(string.Format("datasets/{0}/stations/{1}/datatypes/{2}.json", datasetName, stationId, dataTypeId));
        }
        /// <summary>
        /// Builds the datasets locations stations URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="stationId">The station id.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <remarks></remarks>
        public void BuildDatasetsLocationsStationsUri(string datasetName, string locationId, string stationId, string dataTypeId)
        {
            base.SetCommandUri(string.Format("datasets/{0}/locations/{1}/stations/{2}/datatypes/{3}.json", datasetName, locationId, stationId, dataTypeId));
        }
        /// <summary>
        /// Builds the datasets location types locations URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeName">Name of the location type.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="dataTypeId">The data type id.</param>
        /// <remarks></remarks>
        public void BuildDatasetsLocationTypesLocationsUri(string datasetName, string locationTypeName, string locationId, string dataTypeId)
        {
            base.SetCommandUri(string.Format("datasets/{0}/locationtypes/{1}/locations/{2}/datatypes/{3}.json", datasetName, locationTypeName, locationId, dataTypeId));
        }
        /// <summary>
        /// Inits this instance.
        /// </summary>
        /// <remarks></remarks>
        public override void Init()
        {
            // do nothing
        }
    }
}
