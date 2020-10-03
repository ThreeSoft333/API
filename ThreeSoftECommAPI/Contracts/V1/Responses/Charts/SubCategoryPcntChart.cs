using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.Charts
{
    public class SubCategoryPcntChart
    {
        public string name { get; set; }
        public string id { get; set; }
        public List<object> data { get; set; }
    }
}
