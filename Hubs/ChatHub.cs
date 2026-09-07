using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs;

public class ChatHub : Hub<IChatClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.ConnectionTest(Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public async Task SendToAll(string user, string message)
    {
        await Clients.All.ReceiveMessage(user, message);
    }

    public async Task SendToOthers(string user, string message)
    {
        await Clients.Others.ReceiveOthersMessage(user, message);
        await Clients.Caller.ReceiveCallerNotification($"Message delivered to other clients: '{message}'");
    }

    public async Task SendToCaller(string message)
    {
        await Clients.Caller.ReceiveCallerNotification(message);
    }

    public async Task JoinGroup(string groupName, string? user = null)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task LeaveGroup(string groupName, string? user = null)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task SendToGroup(string groupName, string user, string message)
    {
        await Clients.Group(groupName).ReceiveGroupMessage(groupName, user, message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
