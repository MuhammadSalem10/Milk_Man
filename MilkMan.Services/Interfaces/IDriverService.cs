using MilkMan.Shared.DTOs.Driver;


namespace MilkMan.Application.Interfaces;

public interface IDriverService
{
    Task<DriverDto> GetDriverByIdAsync(int id);
    Task<IEnumerable<DriverDto>> GetAllDriversAsync();
    Task<DriverDto> CreateDriverAsync(CreateDriverDto driverDto);
    Task<DriverDto> UpdateDriverAsync(int id, UpdateDriverDto driverDto);
    Task DeleteDriverAsync(int id);
    Task<IEnumerable<DriverDto>> GetAvailableDriversAsync();
}
