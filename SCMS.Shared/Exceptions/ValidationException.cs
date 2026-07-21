using scms.Shared.Exceptions.Abstract;
using System.Net;

namespace SCMS.Shared.Exceptions;

public sealed class ValidationException : BaseException
{
    public ValidationException(string message)
        : base(message, HttpStatusCode.BadRequest)
    {
        Errors = new List<string> { message };
    }

    public ValidationException(string message, IEnumerable<string> errors)
        : base(message, HttpStatusCode.BadRequest)
    {
        Errors = errors.ToList();
    }

    public List<string> Errors { get; }
}
