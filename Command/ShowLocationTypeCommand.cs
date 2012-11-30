using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models;

namespace NCDCWebService.Command
{
    internal class ShowLocationTypeCommand : NCDCCommand<NCDCLocationType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowLocationTypeCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="locationTypeName">Name of the location type.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ShowLocationTypeCommand(string datasetName, string locationTypeName, string token, Options.NCDCOptions options)
            : base(
            HTTPVerb.GET, 
            string.Format("datasets/{0}/locationtypes/{1}.json", datasetName, locationTypeName),
            token, options)
        {
            this.DeserializationHandler = NCDCLocationType.Deserialize;
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