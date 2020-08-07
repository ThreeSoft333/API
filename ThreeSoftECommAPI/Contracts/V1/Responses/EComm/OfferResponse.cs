using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.EComm
{
    public class OfferResponse
    {
        public Int64 Id { get; set; }
        public string ArabicDesc { get; set; }
        public string EnglishDesc { get; set; }
        public string ImgUrl { get; set; }
        public int status { get; set; }
        public decimal offerPrice { get; set; }
        public ProductResponse product { get; set; }
    }
}
