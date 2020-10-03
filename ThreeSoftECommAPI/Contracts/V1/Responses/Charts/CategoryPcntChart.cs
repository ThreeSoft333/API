using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.Charts
{
    public class CategoryPcntChart
    {
        public string name { get; set; }
        public decimal y { get; set; }
        public string drilldown { get; set; }
    }
}
