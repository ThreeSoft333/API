using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.UserAddressesReq
{
    public class CreateUserAddresseRequest
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public string PostCode { get; set; }
        public decimal Lon { get; set; }
        public decimal Lat { get; set; }
    }
}
