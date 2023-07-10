using System.Runtime.Serialization;

namespace Application.Common.Exceptions;

public class BillingException : Exception, ISerializable
{
    public BillingException()
      : base()
    {
    }
    public BillingException(string message)
        : base(message)
    {
    }
}
