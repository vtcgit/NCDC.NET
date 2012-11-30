using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models;

namespace NCDCWebService.Command
{
    class ShowStationCommand : NCDCCommand<NCDCStation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowStationCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeName">Name of the location type.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="stationId">The station id.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ShowStationCommand(string datasetName, string locationTypeName, string locationId, string stationId, string token, Options.NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/locationtypes/{1}/locations/{2}/stations/{3}.json", datasetName, locationTypeName, locationId, stationId),
            token, options)
        {
            this.DeserializationHandler = NCDCStation.Deserialize;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowStationCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="stationId">The station id.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ShowStationCommand(string datasetName, string locationId, string stationId, string token, Options.NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/locations/{1}/stations/{2}.json", datasetName, locationId, stationId),
            token, options)
        {
            this.DeserializationHandler = NCDCStation.Deserialize;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowStationCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="stationId">The station id.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ShowStationCommand(string datasetName, string stationId, string token, Options.NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/stations/{1}.json", datasetName, stationId),
            token, options)
        {
            this.DeserializationHandler = NCDCStation.Deserialize;
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
