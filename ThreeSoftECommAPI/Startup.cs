using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using ThreeSoftECommAPI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ThreeSoftECommAPI.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ThreeSoftECommAPI.Services.Identity;
using ThreeSoftECommAPI.Domain.Identity;
using ThreeSoftECommAPI.Services.EComm.BrandServ;
using ThreeSoftECommAPI.Services.EComm.AdvertisingServ;
using ThreeSoftECommAPI.Services.EComm.CategoryServ;
using ThreeSoftECommAPI.Services.EComm.ProductServ;
using ThreeSoftECommAPI.Services.EComm.UserFavouritesServ;
using ThreeSoftECommAPI.Services.EComm.SubCategoryServ;
using ThreeSoftECommAPI.Services.EComm.SubSubCategoryServ;
using ThreeSoftECommAPI.Services.EComm.OffersServ;
using ThreeSoftECommAPI.Services.EComm.ProductReviewsServ;
using ThreeSoftECommAPI.Services.EComm.ProductColorServ;
using ThreeSoftECommAPI.Services.EComm.ProductSizeServ;
using ThreeSoftECommAPI.Services.EComm.ProductImageServ;
using ThreeSoftECommAPI.Services.ProductImageServ;
using ThreeSoftECommAPI.Services.EComm.CartServ;
using ThreeSoftECommAPI.Services.EComm.CartItemsServ;
using ThreeSoftECommAPI.Services.EComm.CartItemItemsServ;
using ThreeSoftECommAPI.Services.EComm.UserAddressesServ;
using ThreeSoftECommAPI.Services.EComm.OrderServ;
using ThreeSoftECommAPI.Services.EComm.OrderItemServ;
using ThreeSoftECommAPI.Services.EComm.CouponServ;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using ThreeSoftECommAPI.Services.EComm.ContactUsServ;
using ThreeSoftECommAPI.Services.EComm.PaymentServ;
using ThreeSoftECommAPI.Services.EComm.ProductAttributeServ;
using ThreeSoftECommAPI.Services.EComm.NotificationServ;
using ThreeSoftECommAPI.Services.EComm.UserNotifCountServ;

namespace ThreeSoftECommAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();


            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IAdvertisingService, AdvertisingService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserFavService, UserFavService>();
            services.AddScoped<IOffersService, OffersService>();
            services.AddScoped<IProductReviewService, ProductReviewService>();
            services.AddScoped<IProductColorService, ProductColorService>();
            services.AddScoped<IProductSizeService, ProductSizeService>();
            services.AddScoped<IProductImagesService, ProductImagesService>();
            services.AddScoped<IProductAttributeService, ProductAttributeService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IUserAddressesService, UserAddresseService>();
            services.AddScoped<ICouponServices, CouponServices>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserNotificationCountService, UserNotificationCountService>();


            services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                .SetIsOriginAllowed((host) => true);
            }));

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:4200")
            //        .AllowAnyHeader()
            //        .AllowAnyMethod()
            //        .AllowCredentials()
            //        .SetIsOriginAllowed((host) => true));
            //});

            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(x =>
               {
                   x.SaveToken = true;
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       RequireExpirationTime = false,
                       ValidateLifetime = true
                   };
               });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "ThreeSoftECommAPI", Version = "v1" });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                     {"Bearer",new string[0] }
                };

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                },
                                Scheme= "Authorization",
                                Name="Bearer",
                                In= ParameterLocation.Header
                            },
                            new List<string>()
                    }
                });
            });

            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Stripe.StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["Secretkey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerOptions.JsonRoute;
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
            });

            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Resources")),
                RequestPath = new PathString("/wwwroot/Resources")
            });

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
