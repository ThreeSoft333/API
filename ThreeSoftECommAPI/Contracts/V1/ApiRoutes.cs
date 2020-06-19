using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "V1";
        public const string Base = Root + "/" + Version;

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string CreateRole = Base + "/identity/role";
            public const string GetRole = Base + "/identity/role/{roleId}";
        }

        public static class Dashboard
        {
            public const string Get = Base + "/dashboard";
        }
        public static class Brands
        {
            public const string GetAll = Base + "/brands";
            public const string Get = Base + "/brands/{brandId}";
            public const string Create = Base + "/brands";
            public const string Update = Base + "/brands/{brandId}";
            public const string Delete = Base + "/brands/{brandId}";
        }

        public static class Advertise
        {
            public const string GetAll = Base + "/advertising";
            public const string Get = Base + "/advertising/{advertiseId}";
            public const string Create = Base + "/advertising";
            public const string Update = Base + "/advertising/{advertiseId}";
            public const string Delete = Base + "/advertising/{advertiseId}";
        }

        public static class Category
        {
            public const string GetAll = Base + "/categories";
            public const string GetTob = Base + "/categoriesTop";
            public const string Get = Base + "/category/{catgId}";
            public const string Create = Base + "/category";
            public const string Update = Base + "/category/{catgId}";
            public const string Delete = Base + "/category/{catgId}";
        }

        public static class SubCategory
        {
            public const string GetAll = Base + "/subCategories/{catgId}";
            public const string Get = Base + "/category/{subCatgId}";
            public const string Create = Base + "/subCategory";
            public const string Update = Base + "/subCategory/{subCatgId}";
            public const string Delete = Base + "/subCategory/{subCatgId}";
        }

        public static class Product
        {
            public const string GetAll = Base + "/product/{SubCatgId}";
            public const string ProductsMostRecent = Base + "/productmostrecent";
            public const string ProductMostWanted = Base + "/productmostwanted";
            public const string GetProductTopRated = Base + "/producttoprated";
            public const string Get = Base + "/product/{productId}";
            public const string GetUserFav = Base + "/productUserFav/{UserId}";
            public const string Create = Base + "/product";
            public const string Update = Base + "/product/{productId}";
            public const string Delete = Base + "/product/{productId}";
            public const string ViewProduct = Base + "/viewproduct/{productId}";
        }

        public static class UserFav
        {
            public const string Create = Base + "/UserFav";
            public const string Delete = Base + "/UserFav/{userId}/{productId}";
        }

        public static class Offers
        {
            public const string GetAll = Base + "/offers";
            public const string Get = Base + "/offers/{offerId}";
            public const string Create = Base + "/offers";
            public const string Update = Base + "/offers/{offerId}";
            public const string Delete = Base + "/offers/{offerId}";
        }

        public static class Product_Image
        {
            public const string GetAll = Base + "/productImage/{productId}";
            public const string Get = Base + "/productImage/{id}";
            public const string Create = Base + "/productImage";
            public const string Update = Base + "/productImage/{id}";
            public const string Delete = Base + "/productImage/{id}";
        }

        public static class Product_Review
        {
            public const string GetAll = Base + "/productReview/{productId}";
            public const string Get = Base + "/productReview/{id}";
            public const string Create = Base + "/productReview";
            public const string Update = Base + "/productReview/{id}";
            public const string Delete = Base + "/productReview/{id}";
        }

        public static class Product_Colors
        {
            public const string GetAll = Base + "/productColors/{productId}";
            public const string Get = Base + "/productColors/{id}";
            public const string Create = Base + "/productColors";
            public const string Update = Base + "/productColors/{id}";
            public const string Delete = Base + "/productColors/{id}";
        }

        public static class Product_Sizes
        {
            public const string GetAll = Base + "/productSize/{productId}";
            public const string Get = Base + "/productSize/{id}";
            public const string Create = Base + "/productSize";
            public const string Update = Base + "/productSize/{id}";
            public const string Delete = Base + "/productSize/{id}";
        }

        
    }
}
