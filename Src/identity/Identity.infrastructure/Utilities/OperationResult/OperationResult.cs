using System.Net;

public class OperationResult
{
    public bool Success { get; private set; }
    public string OperationName { get; }
    public string Message { get; private set; }
    public string? ExceptionMessage { get; private set; }
    public DateTime OperationDate { get; } = DateTime.Now;
    public HttpStatusCode Status { get; private set; }
    public object? Object { get; private set; }
    public IEnumerable<object>? List { get; private set; }
    public string? ErrorCode { get; private set; }

    // Constructor
    private OperationResult(string operationName)
    {
        OperationName = operationName;
    }

    // Success Methods
    public OperationResult Succeed(string message, HttpStatusCode status = HttpStatusCode.OK, object? obj = null, IEnumerable<object>? list = null)
    {
        Success = true;
        Message = message;
        Status = status;
        Object = obj;
        List = list;
        return this;
    }

    // Failure Methods
    public OperationResult Failed(string message, HttpStatusCode status = HttpStatusCode.BadRequest, string? exceptionMessage = null, string? errorCode = null, object? obj = null, IEnumerable<object>? list = null)
    {
        Success = false;
        Message = message;
        ExceptionMessage = exceptionMessage;
        ErrorCode = errorCode;
        Status = status;
        Object = obj;
        List = list;
        return this;
    }

    // Static Methods for Convenience
    public static OperationResult CreateSuccess(string message, object? obj = null, IEnumerable<object>? list = null)
    {
        return new OperationResult("Success").Succeed(message, HttpStatusCode.OK, obj, list);
    }

    public static OperationResult CreateFailure(string message, string? exceptionMessage = null, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new OperationResult("Failure").Failed(message, status, exceptionMessage);
    }

    public override string ToString()
    {
        return $"{OperationName}: {(Success ? "Success" : "Failed")} - {Message} (Status: {Status})";
    }
}
