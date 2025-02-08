using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;

namespace MilkMan.Infrastructure.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly MilkManDbContext _dbContext;

        public UnitOfWork(MilkManDbContext dbContext)
        {
            _dbContext = dbContext;
            Categories = new CategoryRepository(_dbContext);
            Products = new ProductRepository(_dbContext);
            Inventory = new InventoryRepository(_dbContext);
            Customers = new CustomerRepository(_dbContext);
            ShoppingCarts = new ShoppingCartRepository(_dbContext);
            Orders = new OrderRepository(dbContext);
            Drivers = new DriverRepository(dbContext);
            Deliveries = new DeliveryRepository(dbContext);
            Addresses = new AddressRepository(dbContext);
            
        }

        public ICategoryRepository Categories { get; private set; }

        public IProductRepository Products { get; private set; }
        public IInventoryRepository Inventory { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IDriverRepository Drivers { get; private set; }
        public IDeliveryRepository Deliveries { get; private set; }
        public IAddressRepository Addresses { get; private set; }
        public IRefundRepository Refunds { get; private set; }
        public IReturnRequestRepository ReturnRequests { get; private set; }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }

