using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProductAttr
{
    public class UpdateProductAttributeRequest
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }
}
