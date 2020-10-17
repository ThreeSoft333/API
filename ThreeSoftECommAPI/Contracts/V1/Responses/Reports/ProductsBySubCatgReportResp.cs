using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.Reports
{
    public class ProductsBySubCatgReportResp
    {
        public string productName { get; set; }
        public Int32 producCountAll { get; set; }
        public Int32 producCountAvailable { get; set; }
        public decimal currentPrice { get; set; }
    }
}
