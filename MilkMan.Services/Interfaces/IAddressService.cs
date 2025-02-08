

using MilkMan.Shared.Common;
using MilkMan.Shared.DTOs;
using MilkMan.Shared.DTOs.Address;

namespace MilkMan.Application.Interfaces
{
    public interface IAddressService
    {
        Task<Result<AddressDto>> CreateAddress(CreateAddressDto addressDto);
        Task<AddressDto> UpdateAddress(UpdateAddressDto addressDto);
        Task<IEnumerable<AddressDto>> GetAllAddresses();
        Task<AddressDto?> GetAddressById(int id);
        Task DeleteAddress(int addressId);
    }
}
