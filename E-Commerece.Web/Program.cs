using System.Reflection;
using System.Text;
using Abstraction;
using Domain.Contracts;
using Domain.Models.Identiy;
using E_Commerece.Web;
using E_Commerece.Web.CustomMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Services.MappingProfiles;
using Shared.ErrorModels;
using StackExchange.Redis;

namespace E_Commerece.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region D1 container Services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDBContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("IdentityConnection");
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
            #endregion

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AssemblyReferences).Assembly);
            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
            builder.Services.AddScoped<IServicesManger, ServicesManger>();
            builder.Services.AddScoped<IDBInitializer, DBInitializer>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnectionString"));
            });

            // JWT Config Binding
            var jwtSection = builder.Configuration.GetSection("JWTOptions");
            var jwtOptions = jwtSection.Get<JWTOptions>();
            builder.Services.Configure<JWTOptions>(jwtSection);

            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true
                };
            });

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(m => m.Value.Errors.Any())
                        .Select(m => new ValidationErrror
                        {
                            Field = m.Key,
                            Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                        });

                    var response = new ValiadtionErrorToReturn
                    {
                        ValidationErrrors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            var app = builder.Build();
            await InailizeDbAsync(app);

            #region MiddleWare -Configure Pipelines
            app.UseMiddleware<CustomExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication(); // ? Required before UseAuthorization
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            await app.RunAsync();
        }

        public static async Task InailizeDbAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
            await dbInializer.InializeAsync();
            await dbInializer.IdentityInializeAsync();
        }
    }
}
