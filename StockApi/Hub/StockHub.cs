using Microsoft.AspNetCore.SignalR;

namespace StockApi;

public class StockHub:Hub
{
    public async Task SendData(string message)
    {
        await Clients.All.SendAsync("RecieveMessage",message);
    }
}
