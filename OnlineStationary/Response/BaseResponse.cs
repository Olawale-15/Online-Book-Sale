namespace OnlineStationary.Response
{
    public class BaseResponse
    {
        public string Message { get; set; } = default!;
        public bool IsSucessful { get; set; }
    }
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = default!;
        public bool IsSucessful { get; set; }
    }
}
