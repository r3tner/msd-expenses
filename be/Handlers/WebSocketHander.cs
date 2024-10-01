using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

public static class WebSocketHandler
{
    private static ConcurrentDictionary<string, WebSocket> _clients = [];

    public static async Task ConnectNewClient(WebSocket client, string userId)
    {
        _clients[userId] = client;

        var buffer = new byte[1024 * 4];

        try
        {
            while (client.State == WebSocketState.Open)
            {
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    _clients.TryRemove(userId, out _);
                    Console.WriteLine($"WebSocket removed for client with user ID: {userId}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect WebSocket for client with user ID {userId}: {ex.Message}");
        }
    }

    public static async Task SendExpenseList(string userId, List<Expense> expenses)
    {
        if (_clients.TryGetValue(userId, out var socket) && socket.State == WebSocketState.Open)
        {
            var message = JsonSerializer.Serialize(expenses);
            var bytes = Encoding.UTF8.GetBytes(message);
            var buffer = new ArraySegment<byte>(bytes);

            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}