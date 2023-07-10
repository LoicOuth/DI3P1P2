using System.Runtime.Serialization;

namespace Application.Common.Exceptions;

public class ForbiddenAccessException : Exception, ISerializable
{
    public ForbiddenAccessException() : base("Forbiden Access") { }
}
