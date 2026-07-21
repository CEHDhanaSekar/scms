using scms.Shared.Exceptions.Abstract;
using System.Net;

namespace SCMS.Shared.Exceptions;

public sealed class UnauthorizedException : BaseException
{
    public UnauthorizedException(string message)
        : base(message, HttpStatusCode.Unauthorized)
    {
    }
}
