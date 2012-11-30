using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models;

namespace NCDCWebService.Command
{
    class ShowLocationCommand : NCDCCommand<NCDCLocation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowLocationCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeName">Name of the location type.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ShowLocationCommand(string datasetName, string locationTypeName, string locationId, string token, Options.NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/locationtypes/{1}/locations/{2}.json", datasetName, locationTypeName, locationId),
            token, options)
        {
            this.DeserializationHandler = NCDCLocation.Deserialize;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowLocationCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationId">The location id.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ShowLocationCommand(string datasetName, string locationId, string token, Options.NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/locations/{1}.json", datasetName, locationId),
            token, options)
        {
            this.DeserializationHandler = NCDCLocation.Deserialize;
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
