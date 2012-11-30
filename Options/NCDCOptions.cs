using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCDCWebService.Options
{
    public class NCDCOptions
    {
        public NCDCOptions()
        {
            BaseUrl = @"http://www.ncdc.noaa.gov/cdo-services/services/";
            TokenFormat = @"token={0}";
        }

        public string BaseUrl { get; set; }
        public string TokenFormat { get; set; }
        public int Page { get; set; }
    }
}
