using MilkMan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkMan.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(int customerId, bool trackChanges);
        Task<Customer?> GetByUserIdAsync(string userId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
