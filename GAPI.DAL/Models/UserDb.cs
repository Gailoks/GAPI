namespace GAPI.DAL.Models;

public record UserDb(string Email, byte[] PasswordDigest, string Name, DateTimeOffset? SubscriptionUntil);