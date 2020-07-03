using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProductReq
{
    public class CreateProductRequest
    {
        public Int64 SubCategoryId { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string ImgUrl { get; set; }
        public string Condition { get; set; }
        public string Material { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 status { get; set; }
        public Int64? colorId { get; set; }
        public Int64? sizeId { get; set; }
    }
}
