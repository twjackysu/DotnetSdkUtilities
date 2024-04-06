namespace DotnetSdkUtilities.Factory.ResponseFactory
{
    public interface IApiResponseFactory
    {
        IApiOkResponse<T> CreateOKResponse<T>(T data);
        IApiErrorResponse<T> CreateErrorResponse<T>(T code, string message = "");
    }
}
