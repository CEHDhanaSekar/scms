using scms.Shared.Exceptions.Abstract;
using System.Net;

namespace SCMS.Shared.Exceptions;

public sealed class ConflictException : BaseException
{
    public ConflictException(string message)
        : base(message, HttpStatusCode.Conflict)
    {

    }
}