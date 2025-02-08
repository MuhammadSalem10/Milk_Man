using Microsoft.EntityFrameworkCore;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;


namespace MilkMan.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MilkManDbContext _dbContext;

        public CustomerRepository(MilkManDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            await _dbContext.Set<Customer>().AddAsync(customer);
            return customer;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId, bool trackChanges)
        {
           var customer = await _dbContext.Set<Customer>().Include(c => c.User).Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == customerId);
            return customer;
        }

   

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _dbContext.Set<Customer>()
            .Include(c => c.User)
            .Include(c => c.Address)
            .ToListAsync();
        }

        public async Task<Customer?> GetByUserIdAsync(string userId)
        {
            return await _dbContext.Set<Customer>().Include(c => c.User).Include(c => c.Address)
            .FirstOrDefaultAsync(c => c.User.Id == userId);
        }

        public void Update(Customer customer)
        {
            _dbContext.Set<Customer>().Update(customer);
        }

        public void Delete(Customer customer)
        {
            _dbContext.Set<Customer>().Remove(customer);
        }
        
    }
}
