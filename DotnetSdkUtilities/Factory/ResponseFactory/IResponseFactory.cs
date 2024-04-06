namespace DotnetSdkUtilities.Factory.ResponseFactory
{
    public interface IResponseFactory<TIActionResult>
    {
        TIActionResult CreateOKResponse();
        TIActionResult CreateOKResponse<T>(T data);
        TIActionResult CreateErrorResponse<T>(T code, string message = "");
    }
}
