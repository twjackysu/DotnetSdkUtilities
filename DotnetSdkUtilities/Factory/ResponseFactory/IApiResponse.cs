namespace DotnetSdkUtilities.Factory.ResponseFactory
{
    public interface IApiOkResponse<T>
    {
        T Data { get; set; }
    }
    public interface IApiErrorResponse<T>
    {
        ErrorDetails<T> Error { get; set; }
    }
}
