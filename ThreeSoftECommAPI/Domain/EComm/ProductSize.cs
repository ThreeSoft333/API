using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class ProductSize
    {
        [Key]
        public Int64 Id { get; set; }
        public string Size { get; set; }
        public string Unit { get; set; }
        public Int32 CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category category { get; set; }
    }
}
