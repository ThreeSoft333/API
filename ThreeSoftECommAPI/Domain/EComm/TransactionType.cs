using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class TransactionType
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 Type { get; set; } //1-Charge 2-Refund
    }
}
