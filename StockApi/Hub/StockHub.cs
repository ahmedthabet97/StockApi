using Microsoft.AspNetCore.SignalR;

namespace StockApi;

public class StockHub:Hub
{
    public async Task CrudData(string message)
    {

        await Clients.All.SendAsync("RecieveMessage","Hello World");
    }
}
