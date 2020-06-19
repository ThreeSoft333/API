using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.BrandReq
{
    public class UpdateBrandRequest
    {
        [Required]
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string ImgUrl { get; set; }
    }
}
