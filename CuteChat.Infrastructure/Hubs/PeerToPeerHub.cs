using CuteChat.Infrastructure.Hubs.MessagesStorage;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace CuteChat.Infrastructure.Hubs;
public class PeerToPeerHub : Hub
{
    private static readonly Dictionary<string, string> _userConnections = new();

    public Task RegisterUser(string userId)
    {
        _userConnections[userId] = Context.ConnectionId;
        return Task.CompletedTask;
    }

    public async Task SendMessage(string senderId, string receiverId, string message)
    {
        var sendTasks = new List<Task>();

        if(_userConnections.TryGetValue(senderId, out var sender))
            sendTasks.Add(Clients.Client(sender).SendAsync("ReceiveMessage", senderId, message));
        if(_userConnections.TryGetValue(receiverId, out var receiver))
            sendTasks.Add(Clients.Client(receiver).SendAsync("ReceiveMessage", senderId, message));

        BackgroundJob.Enqueue<IMessageManager>(m => m.SaveMessageAsync(int.Parse(senderId), int.Parse(receiverId), message));

        await Task.WhenAll(sendTasks);
    }
}
