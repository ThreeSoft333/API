using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.OfferReq
{
    public class UpdateOfferRequest
    {
        public string ArabicDesc { get; set; }
        public string EnglishDesc { get; set; }
        public string ImgUrl { get; set; }
        public int status { get; set; }
        public decimal offerPrice { get; set; }

        public Int64 ProductId { get; set; }


    }
}
