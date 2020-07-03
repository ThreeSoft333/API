using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.OffersServ
{
   public interface IOffersService
    {
        Task<List<Offers>> GetOffersAsync(int status);
        Task<List<OfferResponse>> GetOffersTopAsync(string UserId, int count);
        Task<List<OfferResponse>> GetOffersAllForAppAsync(string UserId);
        Task<Offers> GetOffersByIdAsync(Int64 OfferId);
        Task<int> CreateOffersAsync(Offers offer);
        Task<int> UpdateOffersAsync(Offers offer);
        Task<bool> DeleteOffersAsync(Int64 OfferId);
    }
}
