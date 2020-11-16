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
            public const string LoginWeb = Base + "/identity/loginweb";
            public const string Register = Base + "/identity/register";
            public const string RegisterWeb = Base + "/identity/registerweb";
            public const string Update = Base + "/identity/update";
            public const string UpdateFcmRegToken = Base + "/identity/updateFcmRegToken";
            public const string ChangePassword = Base + "/identity/changepassword";
            public const string ResetPassword = Base + "/identity/resetpassword";
            public const string ConfirmPhone = Base + "/identity/confirmphone";
            public const string CreateRole = Base + "/identity/role";
            public const string GetRole = Base + "/identity/role/{roleId}";
            public const string Upload = Base + "/identity/upload";
            public const string SendMessage = Base + "/identity/sendmessage";
            public const string ResendPhoneToken = Base + "/identity/resendphonetoken/{userId}";
            public const string CustomerList = Base + "/identity/customerlist";
        }

        public static class Dashboard
        {
            public const string Get = Base + "/dashboard";
            public const string drillDownChart = Base + "/dashboard/drilldownchart";
        }
        public static class Brands
        {
            public const string GetAll = Base + "/brands";
            public const string Get = Base + "/brands/{brandId}";
            public const string Create = Base + "/brands";
            public const string Upload = Base + "/brand/upload";
            public const string Update = Base + "/brands/{brandId}";
            public const string Delete = Base + "/brands/{brandId}";
        }

        public static class Advertise
        {
            public const string GetAll = Base + "/advertising";
            public const string Get = Base + "/advertising/{advertiseId}";
            public const string Create = Base + "/advertising";
            public const string Update = Base + "/advertising/{advertiseId}";
            public const string Upload = Base + "/advertising/upload";
            public const string Delete = Base + "/advertising/{advertiseId}";
        }

        public static class Category
        {
            public const string GetAll = Base + "/categories";
            public const string search = Base + "/searchcategories";
            public const string GetCatgPag = Base + "/categoriesPag";
            public const string GetTob = Base + "/categoriesTop";
            public const string Get = Base + "/category/{catgId}";
            public const string Create = Base + "/category";
            public const string Update = Base + "/category/{catgId}";
            public const string Upload = Base + "/category/upload";
            public const string Delete = Base + "/category/{catgId}";
            
        }

        public static class SubCategory
        {
            public const string GetAll = Base + "/subCategories/{catgId}";
            public const string search = Base + "/searchsubCategories/{catgId}";
            public const string GetSubCatgPag = Base + "/subCategoriesPag/{catgId}";
            public const string Get = Base + "/subcategory/{id}";
            public const string Create = Base + "/subcategory";
            public const string Update = Base + "/subcategory/{id}";
            public const string Upload = Base + "/subcategory/upload";
            public const string Delete = Base + "/subcategory/{id}";
        }

        public static class Product
        {
            public const string GetAll = Base + "/productsbycategory/{SubCatgId}";
            public const string GetAllforApp = Base + "/productsbysubcategory/{SubCatgId}";
            public const string SearchProduct = Base + "/searchproduct/{UserId}";
            public const string ProductsMostRecent = Base + "/productmostrecent";
            public const string ProductMostWanted = Base + "/productmostwanted";
            public const string GetProductTopRated = Base + "/producttoprated";
            public const string Get = Base + "/product/{productId}";
            public const string GetUserFav = Base + "/productUserFav/{UserId}";
            public const string Create = Base + "/product";
            public const string Update = Base + "/product/{productId}";
            public const string Upload = Base + "/product/upload";
            public const string UploadImages = Base + "/product/uploadimages/{productId}";
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
            public const string GetByProduct = Base + "/offers/{productId}";
            public const string GetForApp = Base + "/getoffersApp";
            public const string Get = Base + "/offers/{offerId}";
            public const string Create = Base + "/offers";
            public const string Update = Base + "/offers/{offerId}";
            public const string Upload = Base + "/offers/upload";
            public const string Delete = Base + "/offers/{offerId}";
        }

        public static class Coupon
        {
            public const string GetAll = Base + "/coupon";
            public const string Get = Base + "/coupon/{couponId}";
            public const string GetByName = Base + "/chekcoupon/{name}";
            public const string Create = Base + "/coupon";
            public const string Update = Base + "/coupon/{couponId}";
            public const string Delete = Base + "/coupon/{couponId}";
        }

        public static class Product_Image
        {
            public const string GetAll = Base + "/productImage/{productId}";
            public const string Get = Base + "/productImage/{id}";
            public const string Create = Base + "/productImage";
            public const string Update = Base + "/productImage/{id}";
            public const string Delete = Base + "/productImage/{id}";
            public const string DeleteByProduct = Base + "/DeleteByproduct/{productId}";
        }

        public static class Product_Review
        {
            public const string GetAll = Base + "/productReview/{productId}";
            public const string Get = Base + "/productReview/{id}";
            public const string GetNew = Base + "/productReview";
            public const string Create = Base + "/productReview";
            public const string Update = Base + "/productReview/{id}";
            public const string Delete = Base + "/productReview/{id}";
        }

        public static class Product_Colors
        {
            public const string GetAll = Base + "/productColors";
            public const string Get = Base + "/productColors/{id}";
            public const string Create = Base + "/productColors";
            public const string Update = Base + "/productColors/{id}";
            public const string Delete = Base + "/productColors/{id}";
        }

        public static class Product_Sizes
        {
            public const string GetAll = Base + "/productSize/";
            public const string Get = Base + "/productSize/{id}";
            public const string Create = Base + "/productSize";
            public const string Update = Base + "/productSize/{id}";
            public const string Delete = Base + "/productSize/{id}";
        }

        public static class CartRoute
        {
            public const string GetAll = Base + "/Cart/{productId}";
            public const string GetCartItemByUser = Base + "/cartItem/{userId}";
            public const string Get = Base + "/Cart/{cartId}";
            public const string Create = Base + "/Cart";
            public const string Update = Base + "/cartItem/{cartItemId}";
            public const string Delete = Base + "/cartItem/{cartItemId}";
        }

        public static class OrderRoute
        {
            public const string Create = Base + "/order";
            public const string Update = Base + "/updateorder";
            public const string ReOrder = Base + "/reorder";
            public const string ReOrderActive = Base + "/reorderactive";
            public const string MyOrder = Base + "/myorder/{userId}";
            public const string OrdersForAdmin = Base + "/getorderforadmin/{status}";
            public const string OrderStatus = Base + "/orderstatus/{userId}";
            public const string OrderStatusChart = Base + "/orderstatuschart";
            public const string CheckPreviousOrder = Base + "/checkpreviousorder/{userId}";
            public const string orderItems = Base + "/orderitems/{orderId}";
            public const string orderStatusCount = Base + "/orderstatuscount";
        }

        public static class UserAddresse
        {
            public const string GetAll = Base + "/useraddresses/{userId}";
            public const string Get = Base + "/useraddresse/{id}";
            public const string Create = Base + "/useraddresses";
            public const string Delete = Base + "/useraddresse/{id}";
        }

        public static class ContactUsRout
        {
            public const string Get = Base + "/contactus";
            public const string Create = Base + "/contactus";
            public const string Update = Base + "/contactus/{id}";
        }

        public static class Product_Attr
        {
            public const string GetAll = Base + "/productattribute/{productId}";
            public const string Create = Base + "/productattribute";
            public const string Update = Base + "/productattribute/{id}";
            public const string Delete = Base + "/productattribute/{id}";
        }

        public static class payment
        {
            public const string Charge = Base + "/charge";
        }

        public static class NotificationRout
        {
            public const string GetAll = Base + "/notifications";
            public const string Get = Base + "/notifications/{id}";
            public const string Create = Base + "/notifications";
            public const string Update = Base + "/notifications/{id}";
            public const string Upload = Base + "/notifications/upload";
            public const string Delete = Base + "/notifications/{id}";
            public const string updateusernotificationcount = Base + "/notifications/updateusernotificationcount/{userid}";
        }

        public static class ReportsRout
        {
            public const string orderReport = Base + "/orderreport";
            public const string orderProductReport = Base + "/orderproductreport";
            public const string ProductBySubCategoryReport = Base + "/productbysubcategoryreport";
        }
    }
}
