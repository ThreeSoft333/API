using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.EComm
{
    public class ProductResponse
    {
        public Int64 Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string ImgUrl { get; set; }
        public Int64 UserFavId { get; set; }

        public string ProductRate { get; set; }
        public List<ProductImage> productImages { get; set; }
        public ProductColors productColor { get; set; }
        public ProductSize productSize { get; set; }
        public List<ProductAttributes> productAttributes { get; set; }

    }
}
