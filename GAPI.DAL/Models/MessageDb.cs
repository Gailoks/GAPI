namespace GAPI.DAL.Models;
using GAPI.Domain;


public record MessageDb(int Index, MessageRole Role, string Text, Guid SessionId);