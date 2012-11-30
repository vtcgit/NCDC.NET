using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCDCWebService.Models.Interfaces;
using NCDCWebService.Models.Collections;

namespace NCDCWebService.Models
{
    public class NCDCResponse<T> where T : INCDCObject
    {
        public T ResponseObject { get; set; }
        public object Result { get; set; }
        public string RequestUrl { get; set; }
        public string Content { get; set; }

        public string ErrorMessage { get; set; }

        public string Token { get; set; }

        public Command.NCDCCommand<T> Command { get; set; }

        public void UpdateContent()
        {
            if (typeof(T) == typeof(NCDCDataCollection))
            {
                var objects = ResponseObject as NCDCDataCollection;
                foreach (var obj in objects)
                {
                    var station = NCDCStation.GetStationInformation(Command.DataSetName, obj.Station.id, Command.Token, null);
                    obj.Station = station.ResponseObject;
                    var dataType = NCDCDataType.GetDataTypeInformationForDataset(Command.DataSetName, obj.DataType.id, Command.Token, null);
                    obj.DataType = dataType.ResponseObject;
                }
            }
        }
    }
}
