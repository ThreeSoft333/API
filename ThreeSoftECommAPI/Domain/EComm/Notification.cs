using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string BodyAr { get; set; }
        public string BodyEn { get; set; }
        public string ImageUrl { get; set; }
        public string CreateDate { get; set; }
    }
}
