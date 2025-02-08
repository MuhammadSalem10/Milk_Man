using Microsoft.AspNetCore.Mvc;
using MilkMan.Application.Interfaces;
using MilkMan.Shared.DTOs.Driver;

namespace MilkMan.API.Controllers
{
    public class DriversController : ApiController
    {
        private readonly IDriverService _driverService;
        private readonly ILogger<DriversController> _logger;

        public DriversController(IDriverService driverService, ILogger<DriversController> logger)
        {
            _driverService = driverService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetAllDrivers()
        {

            var drivers = await _driverService.GetAllDriversAsync();
            return Ok(drivers);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DriverDto>> GetDriver(int id)
        {

            var driver = await _driverService.GetDriverByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return Ok(driver);

        }

        [HttpPost]
        public async Task<ActionResult<DriverDto>> CreateDriver(CreateDriverDto driverDto)
        {

            var createdDriver = await _driverService.CreateDriverAsync(driverDto);
            return CreatedAtAction(nameof(GetDriver), new { id = createdDriver.Id }, createdDriver);


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriver(int id, UpdateDriverDto driverDto)
        {
            if (id != driverDto.Id)
            {
                return BadRequest();
            }


            var updatedDriver = await _driverService.UpdateDriverAsync(id, driverDto);
            return Ok(updatedDriver);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {

            await _driverService.DeleteDriverAsync(id);
            return NoContent();

        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetAvailableDrivers()
        {

            var availableDrivers = await _driverService.GetAvailableDriversAsync();
            return Ok(availableDrivers);


        }
    }
}
