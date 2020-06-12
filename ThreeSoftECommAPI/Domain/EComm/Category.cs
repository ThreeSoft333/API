using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class Category
    {
        [Key]
        public Int32 Id { get; set; }

        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
