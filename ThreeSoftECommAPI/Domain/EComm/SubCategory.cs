using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class SubCategory
    {
        [Key]
        public Int64 Id { get; set; }
        public Int32 CategoryId { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string ImgUrl { get; set; }
        public int Status { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category category { get; set; }
    }
}
