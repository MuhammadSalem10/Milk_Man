

using AutoMapper;
using MilkMan.Application.Interfaces;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Shared.Common;
using MilkMan.Shared.DTOs.Address;

namespace MilkMan.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AddressDto>> CreateAddress(CreateAddressDto addressDto)
        {
           
            
            var address = _mapper.Map<Address>(addressDto);
            var addedAdress = await _unitOfWork.Addresses.AddAsync(address);
            await _unitOfWork.CompleteAsync();
            return Result<AddressDto>.Success(_mapper.Map<AddressDto>(addedAdress));
        }

      
        public async Task DeleteAddress(int addressId)
        {
            var address = await _unitOfWork.Addresses.GetByIdAsync(addressId, trackChanges: true) ?? throw new Exception("Address is not exist!");
            await _unitOfWork.Addresses.DeleteAsync(address);
            await _unitOfWork.CompleteAsync();
        }

     

        public async Task<AddressDto?> GetAddressById(int id)
        {
            var address = await _unitOfWork.Addresses.GetByIdAsync(id, trackChanges: false);
            return _mapper.Map<AddressDto>(address);
        }

        public async Task<IEnumerable<AddressDto>> GetAllAddresses()
        {
            var addresses = await _unitOfWork.Addresses.GetAllAsync(trackChanges: false); 
            return _mapper.Map<IEnumerable<AddressDto>>(addresses);
        }

       

       

        public async Task<AddressDto> UpdateAddress(UpdateAddressDto addressDto)
        {
           
            var existingAddress = await _unitOfWork.Addresses.GetByIdAsync(addressDto.Id, trackChanges: true) ?? throw new Exception("Address not found!");

            _mapper.Map(addressDto, existingAddress);
            _unitOfWork.Addresses.Update(existingAddress);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<AddressDto>(existingAddress);
            
        }

      
    }
}
