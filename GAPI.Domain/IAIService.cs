namespace GAPI.Domain;

public interface IAIService
{
    public Task<Message> GenerateChatCompetitionAsync(IReadOnlyList<Message> messages);
}
