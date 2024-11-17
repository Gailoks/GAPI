using GAPI.DAL.Models;
using GAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace GAPI.DAL;

class DbSessionRepository : ISessionRepository
{
    private readonly DatabaseContext _context;


    public DbSessionRepository(DatabaseContext context)
    {
        _context = context;
    }


    public async Task<Session> CreateAsync(string userEmail, string? name = null)
    {
        var session = new Session(name);
        var sessionDb = new SessionDb(userEmail, session.Id, session.Name);
        await _context.Sessions.AddAsync(sessionDb);
        return session;
    }

    public async Task EditAsync(Session session, string userEmail, bool shouldUpdateMessages = true)
    {
        var sessionDb = new SessionDb(userEmail, session.Id, session.Name);
        _context.Sessions.Update(sessionDb);
        await _context.SaveChangesAsync();

        if (shouldUpdateMessages)
        {
            var messages = session.Messages.Select((s, i) => new MessageDb(i, s.SenderRole, s.Text, session.Id));

            await _context.Messages.Where(s => s.SessionId == session.Id).ExecuteDeleteAsync();
            await _context.Messages.AddRangeAsync(messages);
        }
    }

    public async Task<IReadOnlyCollection<Session>> GetAsync(string userEmail)
    {
        var dbSessions = await _context.Sessions
            .Where(s => s.UserEmail == userEmail)
            .Join(_context.Messages,
                session => session.Id,
                message => message.SessionId,
                (s, m) => new { Session = s, Message = m }
            )
            .GroupBy(s => s.Session)
            .ToArrayAsync();

        var sessions = dbSessions.Select(s =>
        {
            var messages = s.Select(s => new Message(s.Message.Role, s.Message.Text));
            return new Session(s.Key.Id, messages, s.Key.Name);
        }).ToArray();

        return sessions;
    }

    public async Task RemoveAsync(Session session, string userEmail)
    {
        await _context.Sessions.Select(s => s.UserEmail == userEmail && s.Id == session.Id).ExecuteDeleteAsync();
    }
}
