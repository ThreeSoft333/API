using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.OffersServ
{
    public class OffersService : IOffersService
    {
        private readonly ApplicationDbContext _dataContext;

        public OffersService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<List<Offers>> GetOffersAsync(int status)
        {
            switch (status)
            {
                case 1:
                    return await _dataContext.Offers.Include(p => p.Product).Where(x => x.status == 1).ToListAsync();
                case 0:
                    return await _dataContext.Offers.Include(p => p.Product).Where(x => x.status == 0).ToListAsync();
                default:
                    return await _dataContext.Offers.Include(p => p.Product).ToListAsync();
            }
        }

        public async Task<List<OfferResponse>> GetOffersTopAsync(string UserId, int count)
        {

            if (count != 0)
            {
                var Offers = await (
         from p in _dataContext.product
         join o in _dataContext.Offers on p.Id equals o.ProductId
         where o.status == 1
         /*join f in _dataContext.UserFavourites on p.Id equals f.ProductId into pf
         from x in pf.DefaultIfEmpty()
         where x.UserId == UserId || x.UserId == null*/
         select new OfferResponse
         {
              Id = o.Id,
             ArabicDesc = o.ArabicDesc,
             EnglishDesc = o.EnglishDesc,
             offerPrice = o.offerPrice,
             ImgUrl = o.ImgUrl,
             status = o.status,
             product = new ProductResponse
             {
                 Id = p.Id,
                 ArabicName = p.ArabicName,
                 EnglishName = p.EnglishName,
                 ArabicDescription = p.ArabicDescription,
                 EnglishDescription = p.EnglishDescription,
                 Quantity = p.Quantity,
                 Price = p.Price,
                 SalePrice = p.SalePrice,
                 UserFavId = _dataContext.UserFavourites.SingleOrDefault(x => x.UserId == UserId && x.ProductId == p.Id).Id,
                 Condition = p.Condition,
                 Material = p.Material,
                 ImgUrl = p.ImgUrl,
                 productColor = p.productColor,
                 productSize = p.productSize,
                 productImages = p.productImages,
                 productAttributes = p.productAttributes,
             }
         }).Take(count).ToListAsync();

                return Offers;
            }
            else
            {
                var Offers = await (
        from p in _dataContext.product
        join o in _dataContext.Offers on p.Id equals o.ProductId
        where o.status == 1
        join f in _dataContext.UserFavourites on p.Id equals f.ProductId into pf
        from x in pf.DefaultIfEmpty()
        where x.UserId == UserId || x.UserId == null
        select new OfferResponse
        {
            Id = o.Id,
            ArabicDesc = o.ArabicDesc,
            EnglishDesc = o.EnglishDesc,
            offerPrice = o.offerPrice,
            ImgUrl = o.ImgUrl,
            status = o.status,
            product = new ProductResponse
            {
                Id = p.Id,
                ArabicName = p.ArabicName,
                EnglishName = p.EnglishName,
                ArabicDescription = p.ArabicDescription,
                EnglishDescription = p.EnglishDescription,
                Quantity = p.Quantity,
                Price = p.Price,
                SalePrice = p.SalePrice,
                UserFavId = _dataContext.UserFavourites.SingleOrDefault(x => x.UserId == UserId && x.ProductId == p.Id).Id,
                Condition = p.Condition,
                Material = p.Material,
                ImgUrl = p.ImgUrl,
                productColor = p.productColor,
                productSize = p.productSize,
                productImages = p.productImages,
                productAttributes = p.productAttributes,
            }
        }).ToListAsync();

                return Offers;
            }
        }

        public async Task<List<OfferResponse>> GetOffersAllForAppAsync(string UserId)
        {

            var Offers = await (
        from p in _dataContext.product
        join o in _dataContext.Offers on p.Id equals o.ProductId
        where o.status == 1
        //join f in _dataContext.UserFavourites on p.Id equals f.ProductId into pf
        //from x in pf.DefaultIfEmpty()
        //where x.UserId == UserId || x.UserId == null
        select new OfferResponse
        {
            Id = o.Id,
            ArabicDesc = o.ArabicDesc,
            EnglishDesc = o.EnglishDesc,
            offerPrice = o.offerPrice,
            ImgUrl = o.ImgUrl,
            status = o.status,
            
            product = new ProductResponse
            {
                Id = p.Id,
                ArabicName = p.ArabicName,
                EnglishName = p.EnglishName,
                ArabicDescription = p.ArabicDescription,
                EnglishDescription = p.EnglishDescription,
                Quantity = p.Quantity,
                Price = p.Price,
                SalePrice = p.SalePrice,
                UserFavId = _dataContext.UserFavourites.SingleOrDefault(x => x.UserId == UserId && x.ProductId == p.Id).Id,
                Condition = p.Condition,
                Material = p.Material,
                ImgUrl = p.ImgUrl,
                productColor = p.productColor,
                productSize = p.productSize,
                productImages = p.productImages,
                productAttributes = p.productAttributes,
                

            }
        }).ToListAsync();

            return Offers;
        }

        public async Task<Offers> GetOffersByIdAsync(Int64 OfferId)
        {
            return await _dataContext.Offers.SingleOrDefaultAsync(x => x.Id == OfferId);
        }

        public async Task<int> CreateOffersAsync(Offers Offers)
        {
            var CheckProd = await _dataContext.Offers.SingleOrDefaultAsync(x => x.ProductId == Offers.ProductId);

            if (CheckProd != null)
                return -1;

            await _dataContext.Offers.AddAsync(Offers);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> UpdateOffersAsync(Offers Offers)
        {
            _dataContext.Offers.Update(Offers);
            var Updated = await _dataContext.SaveChangesAsync();
            return Updated;
        }

        public async Task<bool> DeleteOffersAsync(Int64 OfferId)
        {
            var Offer = await GetOffersByIdAsync(OfferId);

            if (Offer == null)
                return false;

            _dataContext.Offers.Remove(Offer);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<List<Offers>> GetOffersByProductIdAsync(long ProductId)
        {
            return await _dataContext.Offers.Include(x => x.Product).Where(x => x.ProductId == ProductId).ToListAsync();
        }
    }
}
