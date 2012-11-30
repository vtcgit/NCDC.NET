using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models.Collections;
using NCDCWebService.Options;
using System.Globalization;

namespace NCDCWebService.Command
{
    internal class ListLocationTypesCommand: NCDCCommand<NCDCLocationTypeCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListLocationTypesCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListLocationTypesCommand(string datasetName, string token, Options.NCDCOptions options)
            : base(
            HTTPVerb.GET, 
            string.Format("datasets/{0}/locationtypes.json", datasetName),
            token, options)
        {
            this.DeserializationHandler = NCDCLocationTypeCollection.Deserialize;
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
