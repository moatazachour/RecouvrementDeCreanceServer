namespace RdC.Domain.Abstrations
{
    public class Result<T>
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public T? Data { get; init; }

        public static Result<T> SuccessResult(T data) =>
            new() { Success = true, Data = data };

        public static Result<T> Failure(string message) =>
            new() { Success = false, Message = message };
    }
}
