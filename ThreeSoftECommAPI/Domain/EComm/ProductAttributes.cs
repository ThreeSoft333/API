using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class ProductAttributes
    {
        [Key]
        public Int32 Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public Int64 ProductId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }


        [ForeignKey(nameof(ProductId))]
        public Product product { get; set; }
    }
}
