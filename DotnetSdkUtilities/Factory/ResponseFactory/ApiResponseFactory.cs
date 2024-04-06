namespace DotnetSdkUtilities.Factory.ResponseFactory
{
    public class ApiResponseFactory : IApiResponseFactory
    {
        public IApiOkResponse<T> CreateOKResponse<T>(T data)
        {
            return new ApiOkResponse<T>() { Data = data };
        }
        public IApiErrorResponse<T> CreateErrorResponse<T>(T code, string message = "")
        {
            return new ApiErrorResponse<T>() { Error = new ErrorDetails<T>(code, message) };
        }
    }
}
