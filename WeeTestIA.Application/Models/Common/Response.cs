namespace WeeTestIA.Application.Models.Common;

public class Response<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? TransactionId { get; set; }
    public short? StatusCode { get; set; }
    public Response()
    {
        Success = default;
        Message = default;
        TransactionId = default;
        Data = typeof(T) == typeof(string) ? (T)(object)string.Empty : default;
    }
}
