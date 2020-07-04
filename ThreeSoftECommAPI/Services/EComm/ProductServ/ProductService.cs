using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ProductServ
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dataContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<List<Product>> GetProductsAsync(Int64 subCategoryId, int status)
        {
            switch (status)
            {
                case 1:
                    return await _dataContext.product.Include(x => x.subCategory).Where(c => c.SubCategoryId == subCategoryId).Where(x => x.status == 1).ToListAsync();
                case 0:
                    return await _dataContext.product.Where(c => c.SubCategoryId == subCategoryId).Where(x => x.status == 0).ToListAsync();
                default:
                    return await _dataContext.product.Where(c => c.SubCategoryId == subCategoryId).ToListAsync();
            }
        }

        public async Task<List<ProductResponse>> GetProductsBySubCategoryAsync(string UserId, long SubCatgId)
        {
            var query = await (from p in _dataContext.product
                               where p.status == 1 && p.SubCategoryId == SubCatgId
                               join f in _dataContext.UserFavourites on p.Id equals f.ProductId into pf
                               from x in pf.DefaultIfEmpty()

                               where x.UserId == UserId || x.UserId == null

                               select new ProductResponse
                               {
                                   Id = p.Id,
                                   ArabicName = p.ArabicName,
                                   EnglishName = p.EnglishName,
                                   ArabicDescription = p.ArabicDescription,
                                   EnglishDescription = p.EnglishDescription,
                                   ImgUrl = p.ImgUrl,
                                   Price = p.Price,
                                   SalePrice = p.SalePrice,
                                   UserFavId = x.Id,
                                   ProductRate = string.Format("{0:0.0}",Convert.ToDecimal(_dataContext.ProductReviews.Where(
                                       x=>x.ProductId ==p.Id).Select(t => t.Rate).Sum()) /
                                       Convert.ToDecimal(_dataContext.ProductReviews.Where(
                                       x => x.ProductId == p.Id).Count())),
                                   productColor = (from c in _dataContext.ProductColors
                                                   where c.Id == p.colorId
                                                   select new ProductColors
                                                   {
                                                       Id = c.Id,
                                                       ArabicName = c.ArabicName,
                                                       EnglishName = c.EnglishName,
                                                       HexCode = c.HexCode
                                                   }).Single(),
                                   productSize = (from s in _dataContext.ProductSizes
                                                  where s.Id == p.sizeId
                                                  select new ProductSize
                                                  {
                                                      Id = s.Id,
                                                      Size = s.Size,
                                                      Unit = s.Unit
                                                  }).Single(),

                                   productImages = (from i in _dataContext.ProductImages
                                                    where i.ProductId == p.Id
                                                    select new ProductImage
                                                    {
                                                        Id = i.Id,
                                                        ImgUrl = i.ImgUrl,
                                                        Ext = i.Ext
                                                    }).ToList(),
                                   productAttributes = (from a in _dataContext.ProductAttributes
                                                        where a.ProductId == p.Id
                                                        select new ProductAttributes
                                                        {
                                                            Id = a.Id,
                                                            ArabicName = a.ArabicName,
                                                            EnglishName = a.EnglishName,

                                                        }).ToList()

                               }).ToListAsync();



            return query;
        }
        public async Task<List<ProductResponse>> GetProductsMostRecentAsync(string UserId, int count)
        {

            var query = await (from p in _dataContext.product
                               where p.status == 1
                               join f in _dataContext.UserFavourites on p.Id equals f.ProductId into pf
                               from x in pf.DefaultIfEmpty()

                               where x.UserId == UserId || x.UserId == null


                               select new ProductResponse
                               {
                                   Id = p.Id,
                                   ArabicName = p.ArabicName,
                                   EnglishName = p.EnglishName,
                                   ArabicDescription = p.ArabicDescription,
                                   EnglishDescription = p.EnglishDescription,
                                   ImgUrl = p.ImgUrl,
                                   Price = p.Price,
                                   SalePrice = p.SalePrice,
                                   UserFavId = x.Id,
                                   ProductRate = string.Format("{0:0.0}",Convert.ToDecimal(_dataContext.ProductReviews.Where(
                                       x => x.ProductId == p.Id).Select(t => t.Rate).Sum() / _dataContext.ProductReviews.Where(
                                       x => x.ProductId == p.Id).Count())),
                                   productColor = (from c in _dataContext.ProductColors
                                                   where c.Id == p.colorId
                                                   select new ProductColors
                                                   {
                                                       Id = c.Id,
                                                       ArabicName = c.ArabicName,
                                                       EnglishName = c.EnglishName,
                                                       HexCode = c.HexCode
                                                   }).Single(),
                                   productSize = (from s in _dataContext.ProductSizes
                                                  where s.Id == p.sizeId
                                                  select new ProductSize
                                                  {
                                                      Id = s.Id,
                                                      Size = s.Size,
                                                      Unit = s.Unit
                                                  }).Single(),

                                   productImages = (from i in _dataContext.ProductImages
                                                    where i.ProductId == p.Id
                                                    select new ProductImage
                                                    {
                                                        Id = i.Id,
                                                        ImgUrl = i.ImgUrl,
                                                        Ext = i.Ext
                                                    }).ToList(),
                                   productAttributes = (from a in _dataContext.ProductAttributes
                                                        where a.ProductId == p.Id
                                                        select new ProductAttributes
                                                        {
                                                            Id = a.Id,
                                                            ArabicName = a.ArabicName,
                                                            EnglishName = a.EnglishName,

                                                        }).ToList()

                               }).Take(count).ToListAsync();



            return query;
        }

        public async Task<List<ProductResponse>> GetProductsMostWantedAsync(string UserId, int count)
        {

            var query = await (from p in _dataContext.product
                               where p.status == 1
                               join f in _dataContext.UserFavourites on p.Id equals f.ProductId into pf
                               from x in pf.DefaultIfEmpty()

                               where x.UserId == UserId || x.UserId == null


                               select new ProductResponse
                               {
                                   Id = p.Id,
                                   ArabicName = p.ArabicName,
                                   EnglishName = p.EnglishName,
                                   ArabicDescription = p.ArabicDescription,
                                   EnglishDescription = p.EnglishDescription,
                                   ImgUrl = p.ImgUrl,
                                   Price = p.Price,
                                   SalePrice = p.SalePrice,
                                   UserFavId = x.Id,
                                   ProductRate = string.Format("{0:0.0}", Convert.ToDecimal(_dataContext.ProductReviews.Where(
                                       x => x.ProductId == p.Id).Select(t => t.Rate).Sum() / _dataContext.ProductReviews.Where(
                                       x => x.ProductId == p.Id).Count())),
                                   productColor = (from c in _dataContext.ProductColors
                                                   where c.Id == p.colorId
                                                   select new ProductColors
                                                   {
                                                       Id = c.Id,
                                                       ArabicName = c.ArabicName,
                                                       EnglishName = c.EnglishName,
                                                       HexCode = c.HexCode
                                                   }).Single(),
                                   productSize = (from s in _dataContext.ProductSizes
                                                  where s.Id == p.sizeId
                                                  select new ProductSize
                                                  {
                                                      Id = s.Id,
                                                      Size = s.Size,
                                                      Unit = s.Unit
                                                  }).Single(),

                                   productImages = (from i in _dataContext.ProductImages
                                                    where i.ProductId == p.Id
                                                    select new ProductImage
                                                    {
                                                        Id = i.Id,
                                                        ImgUrl = i.ImgUrl,
                                                        Ext = i.Ext
                                                    }).ToList(),
                                   productAttributes = (from a in _dataContext.ProductAttributes
                                                        where a.ProductId == p.Id
                                                        select new ProductAttributes
                                                        {
                                                            Id = a.Id,
                                                            ArabicName = a.ArabicName,
                                                            EnglishName = a.EnglishName,

                                                        }).ToList()

                               }).Take(count).ToListAsync();

            return query;
        }

        public async Task<List<ProductResponse>> GetProductsTopRatedAsync(string UserId, int count)
        {
            var query = await (from p in _dataContext.product
                               where p.status == 1
                               join f in _dataContext.UserFavourites on p.Id equals f.ProductId into pf
                               from x in pf.DefaultIfEmpty()

                               where x.UserId == UserId || x.UserId == null


                               select new ProductResponse
                               {
                                   Id = p.Id,
                                   ArabicName = p.ArabicName,
                                   EnglishName = p.EnglishName,
                                   ArabicDescription = p.ArabicDescription,
                                   EnglishDescription = p.EnglishDescription,
                                   ImgUrl = p.ImgUrl,
                                   Price = p.Price,
                                   SalePrice = p.SalePrice,
                                   UserFavId = x.Id,
                                   ProductRate = string.Format("{0:0.0}",Convert.ToDecimal(_dataContext.ProductReviews.Where(
                                       x => x.ProductId == p.Id).Select(t => t.Rate).Sum() / _dataContext.ProductReviews.Where(
                                       x => x.ProductId == p.Id).Count())),
                                   productColor = (from c in _dataContext.ProductColors
                                                   where c.Id == p.colorId
                                                   select new ProductColors
                                                   {
                                                       Id = c.Id,
                                                       ArabicName = c.ArabicName,
                                                       EnglishName = c.EnglishName,
                                                       HexCode = c.HexCode
                                                   }).Single(),
                                   productSize = (from s in _dataContext.ProductSizes
                                                  where s.Id == p.sizeId
                                                  select new ProductSize
                                                  {
                                                      Id = s.Id,
                                                      Size = s.Size,
                                                      Unit = s.Unit
                                                  }).Single(),

                                   productImages = (from i in _dataContext.ProductImages
                                                    where i.ProductId == p.Id
                                                    select new ProductImage
                                                    {
                                                        Id = i.Id,
                                                        ImgUrl = i.ImgUrl,
                                                        Ext = i.Ext
                                                    }).ToList(),
                                   productAttributes = (from a in _dataContext.ProductAttributes
                                                        where a.ProductId == p.Id
                                                        select new ProductAttributes
                                                        {
                                                            Id = a.Id,
                                                            ArabicName = a.ArabicName,
                                                            EnglishName = a.EnglishName,

                                                        }).ToList()

                               }).Take(count).ToListAsync();

            return query;
        }

        public async Task<Product> GetProductByIdAsync(Int64 ProductId)
        {
            return await _dataContext.product.SingleOrDefaultAsync(x => x.Id == ProductId);
        }

        public async Task<ViewProductResponse> ViewProductAsync(long ProductId)
        {

            var prod = await _dataContext.product.SingleOrDefaultAsync(x => x.Id == ProductId);
            var prodAttr = await _dataContext.ProductAttributes.Where(x => x.ProductId == ProductId).ToListAsync();
            var prodColor = await _dataContext.ProductColors.ToListAsync();
            var prodSize = await _dataContext.ProductSizes.ToListAsync();
            //var prodImage = await _dataContext.ProductImages.Where(x => x.ProductId == ProductId).ToListAsync();


            return new ViewProductResponse
            {
                Id = prod.Id,
                ArabicName = prod.ArabicName,
                EnglishName = prod.EnglishName,
                ArabicDescription = prod.ArabicDescription,
                EnglishDescription = prod.EnglishDescription,
                Price = prod.Price,
                SalePrice = prod.SalePrice,
                ImgUrl = prod.ImgUrl,
                Condition = prod.Condition,
                Material = prod.Material,
                Quantity = prod.Quantity,
                productAttributes = prodAttr,
                productColors = prodColor,
                productSize = prodSize
            };

            //    var Product = await (
            //from prod in _dataContext.product
            //join attr in _dataContext.ProductAttributes on prod.Id equals attr.ProductId into prodAttr
            //from ProductAttributes in prodAttr.DefaultIfEmpty()
            //join col in _dataContext.ProductColors on prod.Id equals col.ProductId into prodColor
            //from ProductColors in prodColor.DefaultIfEmpty()
            //join size in _dataContext.ProductSizes on prod.Id equals size.ProductId into prodSize
            //from ProductSizes in prodSize.DefaultIfEmpty()
            //join image in _dataContext.ProductImages on prod.Id equals image.ProductId into prodImage
            //from ProductImages in prodImage.DefaultIfEmpty()

            //where prod.Id == ProductId

            //select new ViewProductResponse
            //{
            //    Id = prod.Id,
            //    ArabicName = prod.ArabicName,
            //    EnglishName = prod.EnglishName,
            //    ArabicDescription = prod.ArabicDescription,
            //    EnglishDescription = prod.EnglishDescription,
            //    Price = prod.Price,
            //    SalePrice = prod.SalePrice,
            //    ImgUrl = prod.ImgUrl,
            //    Condition = prod.Condition,
            //    Material = prod.Material,
            //    Quantity = prod.Quantity,
            //    productAttributes = ProductAttributes,
            //    productColors = ProductColors,
            //    productSize = ProductSizes,
            //    productImage = ProductImages
            //}).ToListAsync();
        }

        public async Task<int> CreateProductAsync(Product Product)
        {
            //var CheckArName = await _dataContext.product.SingleOrDefaultAsync(x => x.ArabicName == Product.ArabicName);
            //var CheckEnName = await _dataContext.product.SingleOrDefaultAsync(x => x.EnglishName == Product.EnglishName);

            //if (CheckArName != null || CheckEnName != null)
            //    return -1;

            await _dataContext.product.AddAsync(Product);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> UpdateProductAsync(Product Product)
        {
            var CheckArName = await _dataContext.product.Where(y => y.Id != Product.Id).SingleOrDefaultAsync(x => x.ArabicName == Product.ArabicName);
            var CheckEnName = await _dataContext.product.Where(y => y.Id != Product.Id).SingleOrDefaultAsync(x => x.EnglishName == Product.EnglishName);

            if (CheckArName != null || CheckEnName != null)
                return -1;

            _dataContext.product.Update(Product);
            var Updated = await _dataContext.SaveChangesAsync();
            return Updated;
        }

        public async Task<bool> DeleteProductAsync(Int64 ProductId)
        {
            var Product = await GetProductByIdAsync(ProductId);

            if (Product == null)
                return false;

            _dataContext.product.Remove(Product);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<List<Product>> GetProductsUserFavAsync(string UserId)
        {
            var entryPoint = (from p in _dataContext.product
                              join uf in _dataContext.UserFavourites on p.Id equals uf.ProductId
                              where uf.UserId == UserId
                              select p);

            return await entryPoint.ToListAsync();
        }





        //public async Task<ReviewProductResponse> ReviewProduct(long ProductId)
        //{
        //    var query = await (from p in _dataContext.product
        //                       where p.status == 1
        //                       join f in _dataContext.UserFavourites on p.Id equals f.ProductId into pf
        //                       from userFav in pf.DefaultIfEmpty()
        //                       join a in _dataContext.ProductAttributes on p.Id equals a.ProductId into pa
        //                       from prodAttr in pa.DefaultIfEmpty()
        //                       join 


        //                       where p.Id == ProductId
        //                       select new ProductResponse
        //                       {
        //                           Id = p.Id,
        //                           ArabicName = p.ArabicName,
        //                           EnglishName = p.EnglishName,
        //                           ArabicDescription = p.ArabicDescription,
        //                           EnglishDescription = p.EnglishDescription,
        //                           ImgUrl = p.ImgUrl,
        //                           Price = p.Price,
        //                           SalePrice = p.SalePrice,
        //                           UserFavId = userFav.Id,

        //                       }).Take(count).ToListAsync();

        //    return query;
        //}
    }
}
