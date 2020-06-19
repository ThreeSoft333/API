using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.AdvertisingServ
{
    public class AdvertisingService : IAdvertisingService
    {
        private readonly ApplicationDbContext _dataContext;

        public AdvertisingService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }
        public async Task<int> CreateAdvertisingAsync(Advertising advertising)
        {
            var CheckArName = await _dataContext.Advertisings.SingleOrDefaultAsync(x => x.ArabicDescription == advertising.ArabicDescription);
            var CheckEnName = await _dataContext.Advertisings.SingleOrDefaultAsync(x => x.EnglishDescription == advertising.EnglishDescription);

            if (CheckArName != null || CheckEnName != null)
                return -1;

            await _dataContext.Advertisings.AddAsync(advertising);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<bool> DeleteAdvertisingAsync(int advrId)
        {
            var Advr = await GetAdvertisingByIdAsync(advrId);

            if (Advr == null)
                return false;

            _dataContext.Advertisings.Remove(Advr);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<List<Advertising>> GetAdvertisingAsync(int status)
        {
            switch (status)
            {
                case 1:
                    return await _dataContext.Advertisings.Where(x =>x.Status == 1).ToListAsync();
                case 0:
                    return await _dataContext.Advertisings.Where(x => x.Status == 0).ToListAsync();
                default:
                    return await _dataContext.Advertisings.ToListAsync();
            }
        }

        public async Task<Advertising> GetAdvertisingByIdAsync(int advrId)
        {
            return await _dataContext.Advertisings.SingleOrDefaultAsync(x => x.Id == advrId);
        }

        public async Task<int> UpdateAdvertisingAsync(Advertising advertising)
        {
            var CheckArName = await _dataContext.Advertisings.Where(y => y.Id != advertising.Id).SingleOrDefaultAsync(x => x.ArabicDescription == advertising.ArabicDescription);
            var CheckEnName = await _dataContext.Advertisings.Where(y => y.Id != advertising.Id).SingleOrDefaultAsync(x => x.EnglishDescription == advertising.EnglishDescription);

            if (CheckArName != null || CheckEnName != null)
                return -1;

            _dataContext.Advertisings.Update(advertising);
            var Updated = await _dataContext.SaveChangesAsync();
            return Updated;
        }
    }
}
