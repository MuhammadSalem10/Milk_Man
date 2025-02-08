using AutoMapper;
using MilkMan.Shared.Constants;
using MilkMan.Shared.DTOs.Auth;
using MilkMan.Shared.Interfaces;
using MilkMan.Application.Interfaces.Identity;
using MilkMan.Shared.Common;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Application.Interfaces;


namespace MilkMan.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public CustomerService(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager loggerManager)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var existingCustomer = await _unitOfWork.Customers.GetCustomerByIdAsync(id, true) ?? throw new KeyNotFoundException("Customer not found.");

            _unitOfWork.Customers.Delete(existingCustomer);
            var user = await _userService.FindByIdAsync(existingCustomer.User.Id);
            if(user != null)
             await _userService.DeleteAsync(user);

        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllCustomersAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
            
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int customerId, bool trackChanges)
        {
            var customer = await _unitOfWork.Customers.GetCustomerByIdAsync(customerId, trackChanges);
            if (customer == null)
            {
                return null;
            }

            return _mapper.Map<CustomerDto?>(customer);
        }

        public async Task<Result<CustomerDto>> RegisterCustomerAsync(RegisterCustomerDto registerUserDto)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(registerUserDto.Email, registerUserDto.Password, registerUserDto.PhoneNumber, Roles.Customer);

                var customer = _mapper.Map<Customer>(registerUserDto);
                customer.User = user;
                

                var addedCustomer = await _unitOfWork.Customers.AddCustomerAsync(customer);
                await _unitOfWork.CompleteAsync();
                return Result<CustomerDto>.Success(_mapper.Map<CustomerDto>(addedCustomer));
            }
            catch (Exception ex)
            {
                return Result<CustomerDto>.Failure($"User Registration Failed, {ex.Message}");
            }

        }

        public async Task<CustomerDto> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto)
        {
            var existingCustomer = await _unitOfWork.Customers.GetCustomerByIdAsync(id, true) ?? throw new KeyNotFoundException("Customer not found.");

            _mapper.Map(updateCustomerDto, existingCustomer);
             _unitOfWork.Customers.Update(existingCustomer);

            return _mapper.Map<CustomerDto>(existingCustomer);
        }

        
    }
}
