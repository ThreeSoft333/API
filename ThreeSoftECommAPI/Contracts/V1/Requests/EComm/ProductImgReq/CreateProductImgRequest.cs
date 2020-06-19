using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProductImgReq
{
    public class CreateProductImgRequest
    {
        public string ImgUrl { get; set; }
        public string Ext { get; set; }
        public Int64 ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
