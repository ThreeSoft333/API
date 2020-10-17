using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Contracts.V1.Responses.Reports;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Helpers;

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
                    return await _dataContext.product.Include(x => x.subCategory)
                                                     .Include(x =>x.productImages)
                                                     .Where(c => c.SubCategoryId == subCategoryId && c.status == 1)
                                                     .ToListAsync();
                case 0:
                    return await _dataContext.product.Include(x => x.subCategory)
                                                     .Include(x => x.productImages)
                                                     .Where(c => c.SubCategoryId == subCategoryId && c.status == 0)
                                                     .ToListAsync();
                default:
                    return await _dataContext.product.Include(x => x.subCategory)
                                                     .Include(x => x.productImages)
                                                     .Where(c => c.SubCategoryId == subCategoryId)
                                                     .ToListAsync();
            }
        }
        public ProductRespPagination GetProductsBySubCategoryAsync(string UserId, 
            long SubCatgId,Pagination pagination)
        {
            var product = PagedList<Product>.ToPagedList(_dataContext.product
                                           .Include(x => x.productAttributes)
                                           .Include(x => x.productReviews)
                                           .Include(x => x.userFavourites)
                                           .Include(x => x.productColor)
                                           .Include(x => x.productSize)
                                           .Include(x => x.productImages)
                                           .Where(x=>x.SubCategoryId == SubCatgId)
                                           ,pagination.PageNumber,pagination.PageSize);

            List<ProductResponse> lstproductResponses = new List<ProductResponse>();
            
            for (int i = 0; i < product.Count; i++)
            {

                var prodRateSum = Convert.ToDecimal(product[i].productReviews.Where(
                                            x => x.ProductId == product[i].Id & x.Status == 1).Select(t => t.Rate).Sum());
                var prodRateCount = Convert.ToDecimal(product[i].productReviews.Where(
                                             x => x.ProductId == product[i].Id & x.Status == 1).Count());

                decimal ProductRate = 0;
                if (prodRateSum != 0 && prodRateCount != 0)
                {
                    ProductRate = prodRateSum / prodRateCount;
                }
                var _rate = string.Format("{0:0.0}", ProductRate);

                var productRespons = new ProductResponse
                {
                    Id = product[i].Id,
                    ArabicName = product[i].ArabicName,
                    EnglishName = product[i].EnglishName,
                    ArabicDescription = product[i].ArabicDescription,
                    EnglishDescription = product[i].EnglishDescription,
                    ImgUrl = product[i].ImgUrl,
                    Price = product[i].Price,
                    SalePrice = product[i].SalePrice,
                    UserFavId = product[i].userFavourites.SingleOrDefault(x => x.UserId == UserId) == null ? 0 : product[i].userFavourites.SingleOrDefault(x => x.UserId == UserId).Id,
                    ProductRate = _rate,
                    productColor = product[i].productColor,
                    productSize = product[i].productSize,
                    productImages = product[i].productImages.Select(x => new ProductImageResponse
                    {
                        ImgUrl = x.ImgUrl,
                        Ext = x.Ext
                    }).ToList(),
                    productAttributes = product[i].productAttributes.Select(x => new ProductAttributesResponse
                    {
                        ArabicName = x.ArabicName,
                        EnglishName = x.EnglishName

                    }).ToList()

                };
                lstproductResponses.Add(productRespons);
            }
            ProductRespPagination productResp = new ProductRespPagination();
            productResp.productResponses = lstproductResponses;
            productResp.PageSize = product.PageSize;
            productResp.TotalCount = product.TotalCount;
            productResp.TotalPage = product.TotalPage;
            productResp.HasNext = product.HasNext;
            productResp.HasPrevious = product.HasPrevious;
            productResp.CurrentPage = product.CurrentPage;

            return productResp;
        }
        public async Task<List<ProductResponse>> GetProductsMostRecentAsync(string UserId, int count)
        {
            var product = await _dataContext.product
                                           .Include(x => x.productAttributes)
                                           .Include(x => x.productReviews)
                                            .Include(x => x.userFavourites)
                                            .Include(x => x.productColor)
                                            .Include(x => x.productSize)
                                            .Include(x => x.productImages)
                                            .Where(x =>x.status ==1 && x.subCategory.Status == 1 
                                            && x.subCategory.category.Status == 1)
                                            .OrderByDescending(x => x.CreatedAt)
                                            .Take(count)
                                            .ToListAsync();

            List<ProductResponse> lstproductResponses = new List<ProductResponse>();

            if(product != null) {
                for (int i = 0; i < product.Count; i++)
                {
                    //x.UserId == UserId ? x.Id : 0

                    var prodRateSum = Convert.ToDecimal(product[i].productReviews.Where(
                                                 x => x.ProductId == product[i].Id & x.Status == 1).Select(t => t.Rate).Sum());
                    var prodRateCount = Convert.ToDecimal(product[i].productReviews.Where(
                                                 x => x.ProductId == product[i].Id & x.Status == 1).Count());

                    decimal ProductRate = 0;
                    if (prodRateSum != 0 && prodRateCount != 0)
                    {
                        ProductRate = prodRateSum / prodRateCount;
                    }
                    var _rate = string.Format("{0:0.0}", ProductRate);

                    var productRespons = new ProductResponse
                    {
                        Id = product[i].Id,
                        ArabicName = product[i].ArabicName,
                        EnglishName = product[i].EnglishName,
                        ArabicDescription = product[i].ArabicDescription,
                        EnglishDescription = product[i].EnglishDescription,
                        ImgUrl = product[i].ImgUrl,
                        Price = product[i].Price,
                        SalePrice = product[i].SalePrice,
                        UserFavId = product[i].userFavourites.SingleOrDefault(x => x.UserId == UserId) == null ? 0 : product[i].userFavourites.SingleOrDefault(x => x.UserId == UserId).Id,
                        ProductRate = _rate,
                        productColor = product[i].productColor,
                        productSize = product[i].productSize,
                        productImages = product[i].productImages.Select(x => new ProductImageResponse
                        {
                            ImgUrl = x.ImgUrl,
                            Ext = x.Ext
                        }).ToList(),
                        productAttributes = product[i].productAttributes.Select(x => new ProductAttributesResponse
                        {
                            ArabicName = x.ArabicName,
                            EnglishName = x.EnglishName

                        }).ToList()

                    };
                    lstproductResponses.Add(productRespons);
                }
            }


            return lstproductResponses;

        }
        public async Task<List<ProductResponse>> GetProductsMostWantedAsync(string UserId, int count)
        {
            var orderItem = _dataContext.OrderItems
                .GroupBy(x => x.ProductId)
                .OrderByDescending(x => x.Count())
                .Take(count)
                .Select(x => x.Key).ToList();

            List<ProductResponse> lstproductResponses = new List<ProductResponse>();

            for (int i = 0; i < orderItem.Count; i++)
            {
                var product = await _dataContext.product
                                          .Include(x => x.productAttributes)
                                          .Include(x => x.productReviews)
                                          .Include(x => x.userFavourites)
                                          .Include(x => x.productColor)
                                          .Include(x => x.productSize)
                                          .Include(x => x.productImages)
                                          .Where(x => x.Id == orderItem[i] && x.status == 1 
                                          && x.subCategory.Status == 1 && x.subCategory.category.Status == 1)
                                          .SingleOrDefaultAsync();
                if (product != null)
                {
                    var prodRateSum = Convert.ToDecimal(product.productReviews.Where(
                                                x => x.ProductId == product.Id & x.Status == 1).Select(t => t.Rate).Sum());
                    var prodRateCount = Convert.ToDecimal(product.productReviews.Where(
                                                 x => x.ProductId == product.Id & x.Status == 1).Count());

                    decimal ProductRate = 0;
                    if (prodRateSum != 0 && prodRateCount != 0)
                    {
                        ProductRate = prodRateSum / prodRateCount;
                    }
                    var _rate = string.Format("{0:0.0}", ProductRate);

                    var productRespons = new ProductResponse
                    {
                        Id = product.Id,
                        ArabicName = product.ArabicName,
                        EnglishName = product.EnglishName,
                        ArabicDescription = product.ArabicDescription,
                        EnglishDescription = product.EnglishDescription,
                        ImgUrl = product.ImgUrl,
                        Price = product.Price,
                        SalePrice = product.SalePrice,
                        UserFavId = product.userFavourites.SingleOrDefault(x => x.UserId == UserId) == null ? 0 : product.userFavourites.SingleOrDefault(x => x.UserId == UserId).Id,
                        ProductRate = _rate,
                        productColor = product.productColor,
                        productSize = product.productSize,
                        productImages = product.productImages.Select(x => new ProductImageResponse
                        {
                            ImgUrl = x.ImgUrl,
                            Ext = x.Ext
                        }).ToList(),
                        productAttributes = product.productAttributes.Select(x => new ProductAttributesResponse
                        {
                            ArabicName = x.ArabicName,
                            EnglishName = x.EnglishName

                        }).ToList()
                    };
                    lstproductResponses.Add(productRespons);
                }
            }


            return lstproductResponses;

           
        }
        public async Task<List<ProductResponse>> GetProductsTopRatedAsync(string UserId, int count)
             
        {
            var product = await _dataContext.product
                                             .Include(x => x.productAttributes)
                                             .Include(x => x.productReviews)
                                              .Include(x => x.userFavourites)
                                              .Include(x => x.productColor)
                                              .Include(x => x.productSize)
                                              .Include(x => x.productImages)
                                               .Where(x => x.status == 1
                                          && x.subCategory.Status == 1 && x.subCategory.category.Status == 1)
                                              .ToListAsync();

            List<ProductResponse> lstproductResponses = new List<ProductResponse>();
            if (product != null) { 

            for (int i = 0; i < product.Count; i++)
            {
                var prodRateSum = Convert.ToDecimal(product[i].productReviews.Where(
                                           x => x.ProductId == product[i].Id & x.Status == 1).Select(t => t.Rate).Sum());
                var prodRateCount = Convert.ToDecimal(product[i].productReviews.Where(
                                             x => x.ProductId == product[i].Id & x.Status == 1).Count());

                decimal ProductRate = 0;
                if (prodRateSum != 0 && prodRateCount != 0)
                {
                    ProductRate = prodRateSum / prodRateCount;
                }
                var _rate = string.Format("{0:0.0}", ProductRate);

                    if (_rate != "0.0")
                    {
                        var productRespons = new ProductResponse
                        {
                            Id = product[i].Id,
                            ArabicName = product[i].ArabicName,
                            EnglishName = product[i].EnglishName,
                            ArabicDescription = product[i].ArabicDescription,
                            EnglishDescription = product[i].EnglishDescription,
                            ImgUrl = product[i].ImgUrl,
                            Price = product[i].Price,
                            SalePrice = product[i].SalePrice,
                            UserFavId = product[i].userFavourites.SingleOrDefault(x => x.UserId == UserId) == null ? 0 : product[i].userFavourites.SingleOrDefault(x => x.UserId == UserId).Id,
                            ProductRate = _rate,
                            productColor = product[i].productColor,
                            productSize = product[i].productSize,
                            productImages = product[i].productImages.Select(x => new ProductImageResponse
                            {
                                ImgUrl = x.ImgUrl,
                                Ext = x.Ext
                            }).ToList(),
                            productAttributes = product[i].productAttributes.Select(x => new ProductAttributesResponse
                            {
                                ArabicName = x.ArabicName,
                                EnglishName = x.EnglishName

                            }).ToList()

                        };
                        lstproductResponses.Add(productRespons);
                    }
                }
        }


            return lstproductResponses.OrderByDescending(x =>x.ProductRate).Take(count).ToList();

           
        }
        public async Task<List<ProductResponse>> SearchProductsAsync(string UserId, string SearchText)
        {
            var product = await _dataContext.product
                                            .Include(x => x.productAttributes)
                                            .Include(x => x.productReviews)
                                            .Include(x => x.userFavourites)
                                            .Include(x => x.productColor)
                                            .Include(x => x.productSize)
                                            .Include(x => x.productImages)
                                            .Where(x => x.status == 1 && x.subCategory.Status == 1 
                                             && x.subCategory.category.Status == 1 &&
                                             x.ArabicName.Contains(SearchText)
                                                    || x.EnglishName.Contains(SearchText)
                                                    || x.ArabicDescription.Contains(SearchText)
                                                    || x.EnglishDescription.Contains(SearchText))
                                             .ToListAsync();

            List<ProductResponse> lstproductResponses = new List<ProductResponse>();
            if (product != null) { 
            

                for (int i = 0; i < product.Count; i++)
                {
                    var prodRateSum = Convert.ToDecimal(product[i].productReviews.Where(
                                              x => x.ProductId == product[i].Id & x.Status == 1).Select(t => t.Rate).Sum());
                    var prodRateCount = Convert.ToDecimal(product[i].productReviews.Where(
                                                 x => x.ProductId == product[i].Id & x.Status == 1).Count());

                    decimal ProductRate = 0;
                    if (prodRateSum != 0 && prodRateCount != 0)
                    {
                        ProductRate = prodRateSum / prodRateCount;
                    }
                    var _rate = string.Format("{0:0.0}", ProductRate);

                    var productRespons = new ProductResponse
                    {
                        Id = product[i].Id,
                        ArabicName = product[i].ArabicName,
                        EnglishName = product[i].EnglishName,
                        ArabicDescription = product[i].ArabicDescription,
                        EnglishDescription = product[i].EnglishDescription,
                        ImgUrl = product[i].ImgUrl,
                        Price = product[i].Price,
                        SalePrice = product[i].SalePrice,
                        UserFavId = product[i].userFavourites.SingleOrDefault(x => x.UserId == UserId) == null ? 0 : product[i].userFavourites.SingleOrDefault(x => x.UserId == UserId).Id,
                        ProductRate = _rate,
                        productColor = product[i].productColor,
                        productSize = product[i].productSize,
                        productImages = product[i].productImages.Select(x => new ProductImageResponse
                        {
                            ImgUrl = x.ImgUrl,
                            Ext = x.Ext
                        }).ToList(),
                        productAttributes = product[i].productAttributes.Select(x => new ProductAttributesResponse
                        {
                            ArabicName = x.ArabicName,
                            EnglishName = x.EnglishName

                        }).ToList()

                    };
                    lstproductResponses.Add(productRespons);
                }
            }


            return lstproductResponses;
        }
        public async Task<Product> GetProductByIdAsync(Int64 ProductId)
        {
            return await _dataContext.product.Include(x=>x.productImages)
                .Include(x=>x.productAttributes)
                .SingleOrDefaultAsync(x => x.Id == ProductId);
        }
        public async Task<ViewProductResponse> ViewProductAsync(long ProductId)
        {

            var prod = await _dataContext.product.Include(x=>x.productColor)
                .Include(x=>x.productSize)
                .SingleOrDefaultAsync(x => x.Id == ProductId);
            var prodAttr = await _dataContext.ProductAttributes.Where(x => x.ProductId == ProductId).ToListAsync();
            
            var prodImage = await _dataContext.ProductImages.Where(x => x.ProductId == ProductId).ToListAsync();


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
                productColors = prod.productColor,
                productSize = prod.productSize,
                productImage = prodImage
            };

          
        }
        public async Task<int> CreateProductAsync(Product Product)
        {
            var CheckArName = await _dataContext.product.Where(x => x.ArabicName == Product.ArabicName).SingleOrDefaultAsync();
            var CheckEnName = await _dataContext.product.Where(x => x.EnglishName == Product.EnglishName).SingleOrDefaultAsync();

            if (CheckArName != null || CheckEnName != null)
                return -1;

            await _dataContext.product.AddAsync(Product);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }
        public async Task<int> UpdateProductAsync(Product Product)
        {
            var CheckArName = await _dataContext.product.Where(y => y.Id != Product.Id && y.ArabicName == Product.ArabicName).SingleOrDefaultAsync();
            var CheckEnName = await _dataContext.product.Where(y => y.Id != Product.Id && y.EnglishName == Product.EnglishName).SingleOrDefaultAsync();

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
        public async Task<List<ProductResponse>> GetProductsUserFavAsync(string UserId)
        {
            var ProdFav = await (from p in _dataContext.product
                                 join uf in _dataContext.UserFavourites on p.Id equals uf.ProductId
                                 where uf.UserId == UserId
                                 join col in _dataContext.ProductColors on p.colorId equals col.Id into ProdCol
                                 from productColor in ProdCol.DefaultIfEmpty()
                                 join size in _dataContext.ProductSizes on p.sizeId equals size.Id into ProdSize
                                 from productSize in ProdSize.DefaultIfEmpty()


                                 select new ProductResponse
                                 {
                                     Id = p.Id,
                                     ArabicName = p.ArabicName,
                                     EnglishName = p.EnglishName,
                                     ArabicDescription = p.ArabicDescription,
                                     EnglishDescription = p.EnglishDescription,
                                     Price = p.Price,
                                     SalePrice = p.SalePrice,
                                     ImgUrl = p.ImgUrl,
                                     ProductRate = string.Format("{0:0.0}", Convert.ToDecimal(_dataContext.ProductReviews.Where(
                                             x => x.ProductId == p.Id).Select(t => t.Rate).Sum()) /
                                             Convert.ToDecimal(_dataContext.ProductReviews.Where(
                                             x => x.ProductId == p.Id).Count())),
                                     productColor = productColor,
                                     productSize = productSize,
                                     productAttributes = _dataContext.ProductAttributes.Where(x => x.ProductId == p.Id)
                                     .Select(x => new ProductAttributesResponse
                                     {
                                         ArabicName = x.ArabicName,
                                         EnglishName = x.EnglishName
                                     }).ToList(),
                                     productImages = _dataContext.ProductImages.Where(x => x.ProductId == p.Id)
                                     .Select(x => new ProductImageResponse
                                     {
                                         ImgUrl = x.ImgUrl,
                                         Ext = x.Ext
                                     })
                                     .ToList(),


                                 }).ToListAsync();

            return ProdFav;
        }
        public async Task<int> UpdateProductSalePriceAsync(long ProductId, decimal salePrice)
        {
            var product = await _dataContext.product.SingleOrDefaultAsync(x => x.Id == ProductId);
            product.SalePrice = salePrice;

            _dataContext.product.Update(product);
            var Updated = await _dataContext.SaveChangesAsync();
            return Updated;
        }
        public async Task<bool> CheckProductsIfActive(long ProductId)
        {
            var produc = await _dataContext.product.Where(x => x.Id == ProductId && x.status == 1 
            && x.subCategory.Status == 1 && x.subCategory.category.Status == 1).SingleOrDefaultAsync();

            if (produc == null)
                return false;

            return true;
        }

        public async Task<List<ProductsBySubCatgReportResp>> ProductsBySubCategoryReport(long subCategoryId)
        {
            return await _dataContext.product
                .Where(x => x.SubCategoryId == subCategoryId)
                .Select(x => new ProductsBySubCatgReportResp
                {
                    productName = x.ArabicName,
                    currentPrice = x.SalePrice != 0 ? x.SalePrice : x.Price,
                    producCountAll = x.Quantity + x.orderItems.Select(x => x.Quantity).Sum(),
                    producCountAvailable = x.Quantity
                }).ToListAsync();
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
