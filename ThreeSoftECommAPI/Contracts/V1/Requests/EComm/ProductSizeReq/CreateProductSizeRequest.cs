using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProductSizeReq
{
    public class CreateProductSizeRequest
    {
        public string Size { get; set; }
        public string Unit { get; set; }
        public Int32 CategoryId { get; set; }
    }
}
