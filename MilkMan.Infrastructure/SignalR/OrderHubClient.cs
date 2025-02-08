using Microsoft.AspNetCore.SignalR;
using MilkMan.Shared.Interfaces;


namespace MilkMan.Infrastructure.SignalR;

    public class OrderHubClient : IOrderHubClient
    {
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderHubClient(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendOrderUpdateAsync(object order)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveOrderUpdate", order);
        }
    }
