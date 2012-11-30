using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models.Collections;
using NCDCWebService.Options;
using System.Globalization;

namespace NCDCWebService.Command
{
    internal class ListLocationsCommand : NCDCCommand<NCDCLocationCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListLocationsCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListLocationsCommand(string datasetName, string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/locations.json", datasetName),
            token, options)
        {
            this.DeserializationHandler = NCDCLocationCollection.Deserialize;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ListLocationsCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeName">Name of the location type.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListLocationsCommand(string datasetName, string locationTypeName, string token, NCDCOptions options)
            : base(
            HTTPVerb.GET,
            string.Format("datasets/{0}/locationtypes/{1}/locations.json", datasetName, locationTypeName),
            token, options)
        {
            this.DeserializationHandler = NCDCLocationCollection.Deserialize;
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
