using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.SubCatgReq
{
    public class CreateSubCategoryRequest
    {
        public Int32 CategoryId { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string ImgUrl { get; set; }
        public int Status { get; set; }
    }
}
