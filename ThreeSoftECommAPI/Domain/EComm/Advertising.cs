using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class Advertising
    {
        [Key]
        public Int32 Id { get; set; }
        public string ArabicDescription { get; set; }
        public string EnglishDescription { get; set; }
        public string ImgUrl { get; set; }
        public Int32 Status { get; set; } //0-deActive 1- Active 

    }
}
