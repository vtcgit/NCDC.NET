using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using NCDCWebService.Models.Interfaces;
using Newtonsoft.Json;

namespace NCDCWebService.Models
{
    public abstract class NCDCObject<T> : INCDCObject
    {
        #region Deserialization
        internal static T Deserialize<T>(JObject value) where T: INCDCObject
        {
            if (value == null || value.First == null || value.First.First["dataSet"] == null && value.First.First["dataSet"].Count() == 0)
                throw new ArgumentNullException("Deserialize Object Cannot Be Null");
            var val = value.First.First["dataSet"].First;
            return JsonConvert.DeserializeObject<T>(val.ToString());
        }
        #endregion
    }
}
