using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models.Interfaces;
using NCDCWebService.Models;
using NCDCWebService.Models.Collections;
using NCDCWebService.Options;
using System.Globalization;

namespace NCDCWebService.Command
{
    [Serializable]
    internal class ListDatasetsCommand : NCDCCommand<NCDCDatasetCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDatasetsCommand"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ListDatasetsCommand(string token, NCDCOptions options)
            : base(HTTPVerb.GET, "datasets.json", token, options)
        {
            this.DeserializationHandler = NCDCDatasetCollection.Deserialize;
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
