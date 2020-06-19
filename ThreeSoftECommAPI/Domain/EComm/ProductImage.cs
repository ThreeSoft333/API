using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class ProductImage
    {
        [Key]
        public Int64 Id { get; set; }
        public string ImgUrl { get; set; }
        public string Ext { get; set; }
        public Int64 ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product  product { get; set; }

    }
}
