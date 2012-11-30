using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models.Interfaces;
using NCDCWebService.Models;

namespace NCDCWebService.Command
{
    public interface ICommand<T>
        where T : INCDCObject
    {
        /// <summary>
        /// Gets the request parameters.
        /// </summary>
        /// <value>The request parameters.</value>
        Dictionary<string, object> RequestParameters { get; }

        /// <summary>
        /// Initializes the command.
        /// </summary>
        void Init();

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        NCDCResponse<T> ExecuteCommand();
    }
}
