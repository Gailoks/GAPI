namespace GAPI.Domain;

public interface IUserRepository
{
    public Task<User> GetAsync(string userEmail);

    public Task CreateAsync(User newUser);

    public Task EditAsync(User user);

    public Task DeleteAsync(User user);
}
