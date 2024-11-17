namespace GAPI.Domain;

public interface ISessionRepository
{
    public Task<IReadOnlyCollection<Session>> GetAsync(string userEmail);

    public Task<Session> CreateAsync(string userEmail, string? name = null);

    public Task EditAsync(Session session, string userEmail, bool shouldUpdateMessages = true);

    public Task RemoveAsync(Session session, string userEmail);
}
