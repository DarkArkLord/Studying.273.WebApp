namespace WebApiUtils.Entities
{
    public class DResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

        public static DResponse<object> Success() => new DResponse<object>
        {
            IsSuccess = true,
            Data = null,
            Message = null,
        };

        public static DResponse<T> Success(T? data) => new DResponse<T>
        {
            IsSuccess = true,
            Data = data,
            Message = null,
        };

        public static DResponse<T> Error(string message, T? data = null) => new DResponse<T>
        {
            IsSuccess = false,
            Data = data,
            Message = message,
        };

        public static DResponse<T> Maybe(bool isSuccess, T? data = null) => new DResponse<T>
        {
            IsSuccess = isSuccess,
            Data = data,
            Message = null,
        };
    }
}
