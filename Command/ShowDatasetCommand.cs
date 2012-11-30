using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models;

namespace NCDCWebService.Command
{
    internal class ShowDatasetCommand : NCDCCommand<NCDCDataset>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShowDatasetCommand"/> class.
        /// </summary>
        /// <param name="datasetName">Name of the dataset.</param>
        /// <param name="token">The token.</param>
        /// <param name="options">The options.</param>
        /// <remarks></remarks>
        public ShowDatasetCommand(string datasetName, string token, Options.NCDCOptions options)
            : base(
            HTTPVerb.GET, 
            string.Format("datasets/{0}.json", datasetName), 
            token, 
            options)
        {
            this.DeserializationHandler = NCDCDataset.Deserialize;
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
