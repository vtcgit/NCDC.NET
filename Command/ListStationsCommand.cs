using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models.Collections;
using NCDCWebService.Options;
using System.Globalization;

namespace NCDCWebService.Command
{
    internal class ListStationsCommand : NCDCCommand<NCDCStationCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListStationsCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="stationTypeName">Name of the station type.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListStationsCommand(string datasetName, string stationTypeName, string locationId, string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/locationtypes/{1}/locations/{2}/stations.json", datasetName, stationTypeName, locationId),
            token, options)
        {
            this.DeserializationHandler = NCDCStationCollection.Deserialize;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ListStationsCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListStationsCommand(string datasetName, string locationId, string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/locations/{1}/stations.json", datasetName, locationId),
            token, options)
        {
            this.DeserializationHandler = NCDCStationCollection.Deserialize;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ListStationsCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListStationsCommand(string datasetName, string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/stations.json", datasetName),
            token, options)
        {
            this.DeserializationHandler = NCDCStationCollection.Deserialize;
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
