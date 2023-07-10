using System.Runtime.Serialization;

namespace Application.Common.Exceptions;

public class AlreadyExistsException : Exception, ISerializable
{
    public AlreadyExistsException()
        : base()
    {
    }

    public AlreadyExistsException(string message)
        : base(message)
    {
    }

    public AlreadyExistsException(string name, string key)
        : base($"Entity \"{name}\" ({key}) already exist")
    {
    }
}