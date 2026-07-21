using scms.Shared.Exceptions.Abstract;
using System.Net;

namespace SCMS.Shared.Exceptions;

public sealed class InternalServerException : BaseException
{
    public InternalServerException(string message)
        : base(message, HttpStatusCode.InternalServerError)
    {
    }
}
