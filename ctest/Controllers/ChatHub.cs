using ctest.Models;
using ctest.Models.dboSchema;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

public class ChatHub : Hub
{
    private readonly Test2Context _context;
    private static ConcurrentDictionary<int, string> userConnections = new ConcurrentDictionary<int, string>();

    public ChatHub(Test2Context context)
    {
        _context = context;
    }

    public override Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var userId = httpContext.Session.GetInt32("UserId");
        if (userId.HasValue)
        {
            userConnections[userId.Value] = Context.ConnectionId;
        }
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var httpContext = Context.GetHttpContext();
        var userId = httpContext.Session.GetInt32("UserId");
        if (userId.HasValue)
        {
            userConnections.TryRemove(userId.Value, out _);
        }
        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string senderUsername, int receiverId, string encryptedMessage)
    {
        try
        {
            var chatMessage = new Message
            {
                // Store the sender's username and other details if needed
                Senderchatuserid = int.Parse(Context.GetHttpContext().Session.GetInt32("UserId").ToString()),
                Receiverchatuserid = receiverId,
                Encryptedcontent = encryptedMessage,
                Timestamp = DateTime.Now,
                Valid = 1
            };

            await _context.Procedures.MessageInsertAsync(chatMessage.Senderchatuserid, chatMessage.Receiverchatuserid, chatMessage.Encryptedcontent, chatMessage.Timestamp, chatMessage.Valid);

            if (userConnections.TryGetValue(receiverId, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", senderUsername, encryptedMessage);
            }
            if (userConnections.TryGetValue((int)chatMessage.Senderchatuserid, out var senderConnectionId))
            {
                await Clients.Client(senderConnectionId).SendAsync("ReceiveMessage", senderUsername, encryptedMessage);
            }
        }
        catch (Exception ex)
        {
            // Error handling
        }
    }
}
