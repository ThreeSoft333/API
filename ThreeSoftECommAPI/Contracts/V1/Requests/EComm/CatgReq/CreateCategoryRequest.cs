using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.CatgReq
{
    public class CreateCategoryRequest
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string ImageUrl { get; set; }
        public int Status { get; set; } // 0- not active 1- active
    }
}
