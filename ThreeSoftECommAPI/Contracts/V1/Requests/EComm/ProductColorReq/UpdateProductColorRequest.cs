using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProductColorReq
{
    public class UpdateProductColorRequest
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string HexCode { get; set; }
    }
}
