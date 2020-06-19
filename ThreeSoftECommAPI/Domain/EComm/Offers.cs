using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class Offers
    {
        [Key]
        public Int64 Id { get; set; }
        public string ArabicDesc { get; set; }
        public string EnglishDesc { get; set; }
        public string ImgUrl { get; set; }
        public int status { get; set; }
        public decimal offerPrice { get; set; }
        public Int64 ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
