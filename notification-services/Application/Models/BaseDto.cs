namespace notification_services.Application.Models
{
    public abstract class BaseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class RequestData<T>
    {
        public Data<T> Data { get; set; }
    }

    public class Data<T>
    {
        public T Attributes { get; set; }
    }
}