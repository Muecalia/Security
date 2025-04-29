namespace Security.Core.Wrappers
{
    public class ApiResponse<T>(T data, bool succeeded, string message)
    {
        public T Data { get; } = data;
        public bool Succeeded { get; } = succeeded;
        public string Message { get; } = message;

        public static ApiResponse<T> Error(string message) => new(default!, false, message);

        public static ApiResponse<T> Success(T data, string message) => new(data, true, message);
    }
}
