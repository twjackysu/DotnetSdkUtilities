namespace DotnetSdkUtilities.Factory.ResponseFactory
{
    public class ErrorDetails<T>
    {
        public ErrorDetails(T code, string message)
        {
            Code = code;
            Message = message;
        }
        public T Code { get; set; }
        public string Message { get; set; }
    }
}
