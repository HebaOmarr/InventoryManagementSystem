
using InventoryManagementSystem.DAL.DataContext;
using InventoryManagementSystem.DAL.UnitOfWork;
using InventoryManagementSystem.Entities.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using InventoryManagementSystem.DAL.Repository.Contract;
using InventoryManagementSystem.DAL.Repository;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using InventoryManagementSystem.BLL.CQRS.Queries.Products;
using Microsoft.AspNetCore.RateLimiting;
using InventoryManagementSystem.API.MiddleWare;
using InventoryManagementSystem.BLL.Notification;
using Hangfire;
using InventoryManagementSystem.BLL.BackgroundJob;
using System.Diagnostics;


namespace InventoryManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApplicationDBContext>(
             options =>
             {
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DataBaseConnectionString"));
             }
             );

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            builder.Services.AddScoped<IInventoryTransactionRepository, InventoryTransactionRepository>();
            builder.Services.AddScoped<IWarehouseProductsRepository, WarehouseProductsRepository>();
            builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<GlobalErrorHandler>();
            builder.Services.AddScoped<INotificarionService, LogNotification>();

            builder.Services.AddMemoryCache();


            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(Program).Assembly,
    typeof(GetAllProductQuery).Assembly
));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddRateLimiter(options =>
            {
                options.AddConcurrencyLimiter("ConcurrencyLimiter", builder =>
                {
                    builder.PermitLimit = 1;
                    builder.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                    builder.QueueLimit = 1;
                }).RejectionStatusCode = 429;

            });

            builder.Services.AddIdentityCore<ApplicationUser>().AddRoles<ApplicationRole>()
                  .AddEntityFrameworkStores<ApplicationDBContext>()
                  .AddDefaultTokenProviders();

         builder.Services.AddHangfire(opt=>opt.UseSqlServerStorage(builder.Configuration.GetConnectionString("DataBaseConnectionString")));
            builder.Services.AddHangfireServer();


            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });


            #region Swagget Setting
            //   builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = " ITI Projrcy"
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                    });
            });
            #endregion

            Serilog.Log.Logger = new LoggerConfiguration()
               .WriteTo.MSSqlServer(
                   connectionString: builder.Configuration.GetConnectionString("DataBaseConnectionString"),
                   sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true }
               ).CreateLogger();

            builder.Host.UseSerilog();
           builder.Services.AddHttpContextAccessor();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //   app.UseMiddleware<GlobalErrorHandler>();
            app.UseHangfireDashboard("/Dashborad");
             
            RecurringJob.AddOrUpdate<LowStockBackgrounJob>("LowStockNotification", x => x.RunTask(), Cron.Minutely);
          //RecurringJob.AddOrUpdate(()=>Debug.WriteLine("Hello World"), Cron.Minutely);

            app.UseRateLimiter();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
