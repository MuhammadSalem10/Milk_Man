

namespace MilkMan.Shared.Interfaces
{
    public interface IOrderHubClient
    {
        Task SendOrderUpdateAsync(object order);
    }
}
