using System.Net;

namespace scms.Shared.Exceptions.Abstract;

public abstract class BaseException : Exception
{
    protected BaseException(
        string message,
        HttpStatusCode statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }
}
