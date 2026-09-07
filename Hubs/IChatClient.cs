namespace ChatApp.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(string user, string message);
    Task ReceiveOthersMessage(string user, string message);
    Task ReceiveGroupMessage(string group, string user, string message);
    Task ReceiveCallerNotification(string message);
    Task SystemNotification(string message);
    Task ConnectionTest(string connectionId);
}
