using Microsoft.AspNetCore.SignalR;

namespace StockApi;

public class StockHub:Hub
{
    public async Task CrudData(object data)
    {

        await Clients.All.SendAsync("ReceiveData", data);
    }
}
