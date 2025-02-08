using MilkMan.Shared.DTOs.Auth;
using MilkMan.Shared.Common;


namespace MilkMan.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<Result<CustomerDto>> RegisterCustomerAsync(RegisterCustomerDto registerUserDto);
        Task<CustomerDto> UpdateCustomerAsync(int id, UpdateCustomerDto customerDto);
        Task DeleteCustomerAsync(int id);
        Task<CustomerDto?> GetCustomerByIdAsync(int customerId, bool trackChanges);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
    }
}
