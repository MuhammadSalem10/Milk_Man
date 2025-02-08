

namespace MilkMan.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        IInventoryRepository Inventory { get; }
        ICustomerRepository Customers { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        IOrderRepository Orders { get; }
        IDriverRepository Drivers { get; }
        IDeliveryRepository Deliveries { get; }
        IAddressRepository Addresses { get; }
        IReturnRequestRepository ReturnRequests { get; }
        IRefundRepository Refunds { get; }
        Task<int> CompleteAsync();
    }
}
