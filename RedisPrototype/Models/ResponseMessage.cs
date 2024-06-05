namespace RedisPrototype.Models
{
    public class ResponseMessage<T> where T : class
    {
        public string Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public string? CacheKey { get; set; }
    }
}
