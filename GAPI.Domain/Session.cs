namespace GAPI.Domain;

public class Session
{
	private readonly List<Message> _messages;


	public Session(string? name = null) : this(Guid.NewGuid(), [], name)
	{
		
	}

	public Session(Guid id, IEnumerable<Message> messages, string? name = null)
	{
		Id = id;
		Name = name;
		_messages = new List<Message>(messages);
	}


	public IReadOnlyList<Message> Messages => _messages;

	public Guid Id { get; }

	public string? Name { get; private set; }


	public void AddMessage(Message message)
	{
		_messages.Add(message);
	}

	public void RemoveMessage(int position)
	{
		_messages.RemoveAt(position);;
	}

	public void EditMessage(int position, Message newMessage)
	{
		_messages[position] = newMessage;
	}

	public void Rename(string? name)
	{
		Name = name;
	}
}
