
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MilkMan.Domain.Repositories;
using MilkMan.Shared.Interfaces;
using MilkMan.Infrastructure.Data;
using MilkMan.Infrastructure.Identity;
using MilkMan.Infrastructure.Repositories;
using MilkMan.Infrastructure.Services;
using MilkMan.Application.Interfaces.Identity;
using MilkMan.Domain.Entities;
using MilkMan.Infrastructure.SignalR;
using Microsoft.Extensions.Logging;

namespace MilkMan.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddIdentity(configuration);

        services.AddScoped<ILoggerManager, LoggerManager>();

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        services.AddTransient<IOrderHubClient, OrderHubClient>();
        services.AddSignalR();
        return services;
    }
    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MilkManDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("MilkManConnectionString"), 
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        })
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, LogLevel.Information));

     services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MilkManDbContext>()
    .AddDefaultTokenProviders();



        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<IReturnRequestRepository, ReturnRequestRepository>();
        services.AddScoped<IRefundRepository, RefundRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IDeliveryRepository, DeliveryRepository>();

        return services;
    }


    private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MilkManDbContext>()
                .AddDefaultTokenProviders();

        return services;
    }


}