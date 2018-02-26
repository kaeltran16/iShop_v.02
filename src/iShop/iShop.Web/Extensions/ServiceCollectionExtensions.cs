using System.Text;
using iShop.Common.Helpers;
using iShop.Data.Entities;
using iShop.Repo;
using iShop.Repo.Data.Implementations;
using iShop.Repo.Data.Interfaces;
using iShop.Repo.UnitOfWork.Implementations;
using iShop.Repo.UnitOfWork.Interfaces;
using iShop.Service.Implementations;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace iShop.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IImagesRepository, ImageRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IShippingRepository, ShippingRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IShippingService, ShippingService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<ISupplierService, SupplierService>();

            return services;
        }

        public static IServiceCollection AddConfigureSettings(this IServiceCollection services)
        {
            services.Configure<ImageSettings>(Startup.Configuration.GetSection("ImageSettings"));
            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            //services.AddSwaggerGen(c =>
            //    c.SwaggerDoc("v1", new Info {Title = "iShop APIs", Description = "API endpoints for iShop"}));
            return services;
        }
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Startup.Configuration.GetConnectionString("Default"),
                    opt => opt.MigrationsAssembly("iShop.Repo"));
            });

            return services;
        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
                {
                    // Custom password requirements
                    opt.Password.RequiredLength = 8;
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.User.RequireUniqueEmail = true;
                })
                // Specify where this data will be stored
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Set the type of claims
                //options.ClaimsIdentity.UserNameClaimType = JwtBearerDefaults.AuthenticationScheme.
                //options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                //options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });
            return services;
        }

        public static IServiceCollection AddCustomAuthenication(this IServiceCollection services)
        {        
            var tokenSettings = new JwtTokenSettings();
            Startup.Configuration.GetSection("JwtTokenSettings").Bind(tokenSettings);
            services.AddSingleton(tokenSettings);

            services.AddAuthentication(opt=>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.Audience = tokenSettings.Audience;
                    opt.Authority = tokenSettings.Authority;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.Key)),
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidAudience = tokenSettings.Audience,
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"                        
                    };

                });

            return services;
        }

        //public static IServiceCollection AddCustomOpenIddict(this IServiceCollection services)
        //{
        //    var tokenSettings = new JwtTokenSettings();
        //    Startup.Configuration.GetSection("JwtTokenSettings").Bind(tokenSettings);
        //    services.AddSingleton(tokenSettings);
        //    services.AddOpenIddict<Guid>(options =>
        //    {
        //        options.AddEntityFrameworkCoreStores<ApplicationDbContext>();

        //        options.AddMvcBinders();
        //        // Enable the token endpoint.
        //        // Form password flow (used in username/password login requests)
        //        options.EnableTokenEndpoint("/connect/token")

        //            // Enable the authorization endpoint.
        //            // Form implicit flow (used in social login redirects)
        //            .EnableAuthorizationEndpoint("/connect/authorize")

        //            .EnableTokenEndpoint("/connect/token")
        //            .EnableUserinfoEndpoint("/api/userinfo");

        //        // Enable the password and the refresh token flows.
        //        options.AllowPasswordFlow()
        //            .AllowAuthorizationCodeFlow()
        //            .AllowRefreshTokenFlow()
        //            // To enable external logins to authenticate
        //            .AllowImplicitFlow();
        //        options.RegisterScopes(OpenIdConnectConstants.Scopes.Profile);
        //        options.DisableHttpsRequirement();
        //        options.AddSigningKey(new SymmetricSecurityKey(Encoding.ASCII.GetBytes((string) tokenSettings.Key)));
        //        options.SetAccessTokenLifetime(TimeSpan.FromMinutes(tokenSettings.TokenLifeTime));
        //        options.SetIdentityTokenLifetime(TimeSpan.FromMinutes(tokenSettings.TokenLifeTime));
        //        options.SetRefreshTokenLifetime(TimeSpan.FromMinutes(tokenSettings.RefreshTokenLifeTime));
        //        options.UseJsonWebTokens();
        //        options.AddEphemeralSigningKey();
        //    });

        //    return services;
        //}

        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy(ApplicationConstants.PolicyName.SuperUsers,
                    opt=>opt.RequireClaim(ApplicationConstants.ClaimName.SuperUser, "true"));
                //cfg.AddPolicy(ApplicationConstants.PolicyName.SuperUsers,
                //    p => p.RequireRole(ApplicationConstants.RoleName.SuperUser));

                cfg.AddPolicy(ApplicationConstants.PolicyName.Users,
                    p => p.RequireClaim(ApplicationConstants.ClaimName.User, "true"));

            });
            return services;
        }
        
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(opt =>
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            return services;
        }
    }
}
