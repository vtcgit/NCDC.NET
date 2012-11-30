using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace NCDCWebService.Models.Interfaces
{
    [Serializable]
    public abstract class NCDCCollection<T> : Collection<T>
        where T : class, INCDCObject
    {
    }
}
