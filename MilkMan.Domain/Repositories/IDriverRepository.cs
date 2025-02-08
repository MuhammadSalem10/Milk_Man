using MilkMan.Domain.Entities;


namespace MilkMan.Domain.Repositories;

public interface IDriverRepository : IRepository<Driver>
{

    Task<Driver?> GetDriverWithOrdersAsync(int id);
    Task<IEnumerable<Driver>> GetAvailableDriversAsync();
}

