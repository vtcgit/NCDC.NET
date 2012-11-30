using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models.Collections;
using NCDCWebService.Options;
using System.Globalization;

namespace NCDCWebService.Command
{
    internal class ListDataTypesCommand : NCDCCommand<NCDCDataTypeCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDataTypesCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeName">Name of the location type.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="stationId">The station id.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListDataTypesCommand(string datasetName, string locationTypeName, string locationId, string stationId, string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/locationtypes/{1}/locations/{2}/stations/{3}/datatypes.json", datasetName, locationTypeName, locationId, stationId),
            token, options)
        {
            this.DeserializationHandler = NCDCDataTypeCollection.Deserialize;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDataTypesCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListDataTypesCommand(string datasetName, string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/datatypes.json", datasetName),
            token, options)
        {
            this.DeserializationHandler = NCDCDataTypeCollection.Deserialize;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ListDataTypesCommand"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListDataTypesCommand(string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            "",
            token, options)
        {
            this.DeserializationHandler = NCDCDataTypeCollection.Deserialize;
        }

        /// <summary>
        /// Builds the datasets location types URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeName">Name of the location type.</param>
        /// <remarks></remarks>
        public void BuildDatasetsLocationTypesUri(string datasetName, string locationTypeName)
        {
            base.SetCommandUri(string.Format("datasets/{0}/locationtypes/{1}/datatypes.json", datasetName, locationTypeName));
        }
        /// <summary>
        /// Builds the datasets locations URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationId">The location id.</param>
        /// <remarks></remarks>
        public void BuildDatasetsLocationsUri(string datasetName, string locationId)
        {
            base.SetCommandUri(string.Format("datasets/{0}/locations/{1}/datatypes.json", datasetName, locationId));
        }
        /// <summary>
        /// Builds the datasets stations URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="stationId">The station id.</param>
        /// <remarks></remarks>
        public void BuildDatasetsStationsUri(string datasetName, string stationId)
        {
            base.SetCommandUri(string.Format("datasets/{0}/stations/{1}/datatypes.json", datasetName, stationId));
        }
        /// <summary>
        /// Builds the datasets locations stations URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="stationId">The station id.</param>
        /// <remarks></remarks>
        public void BuildDatasetsLocationsStationsUri(string datasetName, string locationId, string stationId)
        {
            base.SetCommandUri(string.Format("datasets/{0}/locations/{1}/stations/{2}/datatypes.json", datasetName, locationId, stationId));
        }
        /// <summary>
        /// Builds the datasets location types locations URI.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeName">Name of the location type.</param>
        /// <param name="locationId">The location id.</param>
        /// <remarks></remarks>
        public void BuildDatasetsLocationTypesLocationsUri(string datasetName, string locationTypeName, string locationId)
        {
            base.SetCommandUri(string.Format("datasets/{0}/locationtypes/{1}/locations/{2}/datatypes.json", datasetName, locationTypeName, locationId));
        }

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
