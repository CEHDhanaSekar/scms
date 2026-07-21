namespace scms.Shared.Models;

public class ErrorResponse
{
    public bool Success => false;
    public int StatusCode { get; init; }
    public string Message { get; init; } = string.Empty;
    public List<string>? Errors { get; init; }
}
