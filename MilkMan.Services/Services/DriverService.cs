
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MilkMan.Application.Interfaces;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Shared.DTOs.Driver;

namespace MilkMan.Application.Services;

public class DriverService : IDriverService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public DriverService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<DriverDto> GetDriverByIdAsync(int id)
    {
        var driver = await _unitOfWork.Drivers.GetByIdAsync(id, false);
        return _mapper.Map<DriverDto>(driver);
    }

    public async Task<IEnumerable<DriverDto>> GetAllDriversAsync()
    {
        var drivers = await _unitOfWork.Drivers.GetAllAsync(trackChanges: false);
        return _mapper.Map<IEnumerable<DriverDto>>(drivers);
    }

    public async Task<DriverDto> CreateDriverAsync(CreateDriverDto driverDto)
    {
        var user = new ApplicationUser { UserName = driverDto.Email, Email = driverDto.Email };
        var result = await _userManager.CreateAsync(user, driverDto.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException($"Unable to create user account for driver: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        await _userManager.AddToRoleAsync(user, "Driver");

        var driver = _mapper.Map<Driver>(driverDto);
        driver.User = user;

        await _unitOfWork.Drivers.AddAsync(driver);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<DriverDto>(driver);
    }

    public async Task<DriverDto> UpdateDriverAsync(int id, UpdateDriverDto driverDto)
    {
        var driver = await _unitOfWork.Drivers.GetByIdAsync(id, true);

        if (driver == null)
        {
            throw new Exception($"Driver with id {id} not found");
        }

        _mapper.Map(driverDto, driver);
        _unitOfWork.Drivers.Update(driver);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<DriverDto>(driver);
    }

    public async Task DeleteDriverAsync(int id)
    {
        var driver = await _unitOfWork.Drivers.GetByIdAsync(id, true);

        if (driver == null)
        {
            throw new Exception($"Driver with id {id} not found");
        }

        await _unitOfWork.Drivers.DeleteAsync(driver);
        await _unitOfWork.CompleteAsync();

        // Delete associated user account
        var user = await _userManager.FindByIdAsync(driver.User.Id);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
    }

    public async Task<IEnumerable<DriverDto>> GetAvailableDriversAsync()
    {
        var availableDrivers = await _unitOfWork.Drivers.GetAvailableDriversAsync();
        return _mapper.Map<IEnumerable<DriverDto>>(availableDrivers);
    }

    
}