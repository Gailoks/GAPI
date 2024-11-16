namespace GAPI.Models;

public class User
{
    public List<Session> Sessions {get; set;} = new List<Session>();
}

public class Session
{
    public long SessionId {get; set;}
    public string? SessionName { get; set; }
    public List<Message> Messages { get; set; } = new List<Message>();
}

public class Message
{
    public enum Role
    {
        Model,
        User,
        System
    }
    public Role SenderRole {get; set;}
    public string? Text { get; set; }
}