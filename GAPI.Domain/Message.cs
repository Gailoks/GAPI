namespace GAPI.Domain;

public enum MessageRole
{
	Model,
	User,
	System
}


public record Message(MessageRole SenderRole, string Text);
