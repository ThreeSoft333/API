using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.EComm
{
    public class ProductReviewResponse
    {
        public Int64 Id { get; set; }
        public string ArabicDescreption { get; set; }
        public string EnglishDescreption { get; set; }
        public Int32 Rate { get; set; }
        public Int32 Status { get; set; }
        public string CreatedAt { get; set; }

        public AppUser user { get; set; }

    }
}
