using BlazorWebRtc_Application.Interface.Services.Manager;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace BlazorWebRtc_Application.Hubs;

public class UserHub : Hub
{
    private readonly IConnectionManager _connectionManager;

    public UserHub(IConnectionManager connectionManager) { _connectionManager = connectionManager; }

    public override Task OnConnectedAsync()
    {
        String connectionId = Context.ConnectionId;
        String userId = Context.GetHttpContext()?.Request.Query["userId"].ToString()!;

        _connectionManager.AddConnection(userId!, connectionId);
        List<string> result = _connectionManager.GetAllUserIds();

        Clients.All.SendAsync("UserStatusChanged", JsonConvert.SerializeObject(result), true).GetAwaiter();
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        String connectionId = Context.ConnectionId;
        _connectionManager.RemoveConnection(connectionId);

        List<string> result = _connectionManager.GetAllUserIds();

        Clients.All.SendAsync("UserStatusChanged", JsonConvert.SerializeObject(result), true).GetAwaiter();
        return base.OnDisconnectedAsync(exception);
    }
}