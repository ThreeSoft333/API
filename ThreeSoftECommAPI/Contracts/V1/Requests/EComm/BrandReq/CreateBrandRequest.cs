using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.BrandReq
{
    public class CreateBrandRequest
    {
        [Required]
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }
}
