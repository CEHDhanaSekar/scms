using scms.Shared.Exceptions.Abstract;
using System.Net;

namespace SCMS.Shared.Exceptions;

public sealed class NotFoundException : BaseException
{
    public NotFoundException(string message)
        : base(message, HttpStatusCode.NotFound)
    {
    }
}