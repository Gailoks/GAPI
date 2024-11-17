namespace GAPI.DAL;

using GAPI.DAL.Models;
using GAPI.Domain;
using Microsoft.EntityFrameworkCore;

public class DbUserRepository : IUserRepository
{

    public DbUserRepository(DatabaseContext context)
    {
        _context = context;
    }
    private readonly DatabaseContext _context;


    public async Task<User> GetAsync(string userEmail)
    {
        var dbUser = await _context.Users
            .Where(s => s.Email == userEmail)
            .SingleAsync();
        return new User(dbUser.Email, dbUser.PasswordDigest, dbUser.Name) { SubscriptionUntil = dbUser.SubscriptionUntil };
    }

    public async Task CreateAsync(User newUser)
    {
        var UserDb = new UserDb(newUser.Email, newUser.PasswordDigest, newUser.Name, newUser.SubscriptionUntil);
        await _context.Users.AddAsync(UserDb);
    }

    public async Task EditAsync(User user)
    {
        var dbUser = new UserDb(user.Email, user.PasswordDigest, user.Name, user.SubscriptionUntil);
        _context.Users.Update(dbUser);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        await _context.Users.Where(s => s.Email == user.Email).ExecuteDeleteAsync();
    }
}