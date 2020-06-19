using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class Product
    {
        private const string DefCurr = "JOD";

        [Key]
        public Int64 Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }

        public string ImgUrl { get; set; }

        [DefaultValue(DefCurr)]
        public string Currency { get; set; }
        public string Condition { get; set; }
        public string Material { get; set; }
        public Int32 Quantity { get; set; }

        public Int32 status { get; set; }

        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Int64 SubCategoryId { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        public SubCategory subCategory { get; set; }

    }
}
