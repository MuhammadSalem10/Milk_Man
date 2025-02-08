using Microsoft.Extensions.DependencyInjection;
using MilkMan.Application.Interfaces;
using MilkMan.Application.Services;


namespace MilkMan.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IReturnService, ReturnService>();
            services.AddScoped<IRefundService, RefundService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            return services;
        }



    }
}
