using Microsoft.EntityFrameworkCore;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;

namespace MilkMan.Infrastructure.Repositories
{
    public class AddressRepository(MilkManDbContext context) : Repository<Address>(context), IAddressRepository
    {
    }

  
}
