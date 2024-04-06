namespace DotnetSdkUtilities.Factory.ResponseFactory
{
    public interface IResponseFactory<TIActionResult, TErrorCodes>
    {
        TIActionResult CreateOKResponse();
        TIActionResult CreateOKResponse<T>(T data);
        TIActionResult CreateErrorResponse(TErrorCodes code, string message = "");
    }
}
