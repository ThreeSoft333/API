using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.AdvertisingServ
{
   public interface IAdvertisingService
    {
        Task<List<Advertising>> GetAdvertisingAsync(int status);
        Task<Advertising> GetAdvertisingByIdAsync(Int32 advrId);
        Task<int> CreateAdvertisingAsync(Advertising advertising);
        Task<int> UpdateAdvertisingAsync(Advertising advertising);
        Task<bool> DeleteAdvertisingAsync(Int32 advrId);
    }
}
