using Microsoft.AspNetCore.Mvc;
using MilkMan.Application.Interfaces;
using MilkMan.Shared.DTOs.Address;

namespace MilkMan.API.Controllers;

public class AddressController : ApiController
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress([FromBody] CreateAddressDto createAddressDto)
    {
        var result = await _addressService.CreateAddress(createAddressDto);
        if (result.IsSuccess)
        {
            return CreatedAtAction(nameof(GetAddressById), new { id = result.Value.Id }, result.Value);
        }
        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("areas")]
  

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {

        await _addressService.DeleteAddress(id);
        return NoContent();


    }

    [HttpDelete("areas/{id}")]
   

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddressById(int id)
    {

        var address = await _addressService.GetAddressById(id);
        return Ok(address);


    }

    [HttpGet]
    public async Task<IActionResult> GetAllAddresses()
    {
        var addresses = await _addressService.GetAllAddresses();
        return Ok(addresses);
    }

    [HttpGet("areas")]
    

    [HttpGet("areas/{id}")]
    

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] UpdateAddressDto updateAddressDto)
    {
        if (id != updateAddressDto.Id)
        {
            return BadRequest("Address ID mismatch.");
        }

        var updatedAddress = await _addressService.UpdateAddress(updateAddressDto);
        return Ok(updatedAddress);

    }

  
}

//26.76226544062971, 31.504724684612246

/*
 * 
 * {
  "areaId": 1,
  "addressDetails": "Salah Salem Street, Behind Omar BinelKhattab School.",
  "floorNumber": 1,
  "flatNumber": 1,
  "deliveryInstructions": "Don't bang the door hard!",
  "latitude": 26.76226544062971, 
  "longitude":31.504724684612246
}
 */