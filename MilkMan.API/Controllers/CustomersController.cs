using Microsoft.AspNetCore.Mvc;
using MilkMan.Shared.DTOs.Auth;
using MilkMan.Application.Interfaces;

namespace MilkMan.API.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int customerId)
        {

            var customer = await _customerService.GetCustomerByIdAsync(customerId, trackChanges: false);

            if (customer == null)
            {
                return NotFound(new { Message = "Customer not found." });
            }

            return Ok(customer);


        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerDto registerCustomerDto)
        {

            var createCustomerResult = await _customerService.RegisterCustomerAsync(registerCustomerDto);
                
            if(createCustomerResult.IsFailure)
            {
                return BadRequest(createCustomerResult.ErrorMessage);
            }

            return  Ok(new { message = "New Customer added successfully." });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerDto customerDto)
        {
            if (customerDto == null || customerDto.Id != id)
            {
                return BadRequest();
            }
            var customer = await _customerService.UpdateCustomerAsync(id, customerDto);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }
    }
}

