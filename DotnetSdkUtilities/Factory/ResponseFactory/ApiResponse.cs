namespace DotnetSdkUtilities.Factory.ResponseFactory
{
    public class ApiOkResponse<T> : IApiOkResponse<T>
    {
        public T Data { get; set; }
    }
    public class ApiErrorResponse<T> : IApiErrorResponse<T>
    {
        public ErrorDetails<T> Error { get; set; }
    }
}
