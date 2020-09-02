using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProdReviewReq
{
    public class CreateProductReviewRequest
    {
        public string ArabicDescreption { get; set; }
        public string EnglishDescreption { get; set; }
        public Int32 Rate { get; set; }
        public string UserId { get; set; }
        public Int64 ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
