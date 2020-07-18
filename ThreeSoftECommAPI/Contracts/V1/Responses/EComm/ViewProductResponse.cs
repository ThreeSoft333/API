using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.EComm
{
    public class ViewProductResponse
    {
        public Int64 Id { get; set; }
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

        public List<ProductAttributes> productAttributes { get; set; }
        public ProductColors productColors { get; set; }
        public ProductSize productSize { get; set; }
        public List<ProductImage> productImage { get; set; }
    }
}
