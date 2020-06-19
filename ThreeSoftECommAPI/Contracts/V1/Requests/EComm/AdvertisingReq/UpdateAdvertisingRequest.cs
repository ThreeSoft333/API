using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.AdvertisingReq
{
    public class UpdateAdvertisingRequest
    {
        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }
        public string ImgUrl { get; set; }
        public Int32 Status { get; set; }
    }
}
