﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProductAttr
{
    public class CreateProductAttributeRequest
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public Int64 ProductId { get; set; }
    }
}
