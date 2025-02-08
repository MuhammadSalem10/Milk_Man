

using Microsoft.AspNetCore.SignalR;
using MilkMan.Domain.Entities;

namespace MilkMan.Infrastructure.SignalR;

public class OrderHub : Hub
{
    public async Task SendOrderNotification(Order order)
    {
        await Clients.All.SendAsync("ReceiveOrder", order);
    }
}
