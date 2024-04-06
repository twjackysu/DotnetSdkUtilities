You should create a `ResponseFactory` by yourself and implement `IResponseFactory`.
Then, create an enum for `ErrorCodes`.
Below is an example of usage.

```c#

    public class ResponseFactory : IResponseFactory<IActionResult, ErrorCodes>
    {
        private readonly IApiResponseFactory _apiResponseFactory;
        public ResponseFactory(IApiResponseFactory apiResponseFactory)
        {
            _apiResponseFactory = apiResponseFactory;
        }
        public IActionResult CreateOKResponse()
        {
            return new NoContentResult();
        }
        public IActionResult CreateOKResponse<T>(T data)
        {
            return new OkObjectResult(_apiResponseFactory.CreateOKResponse(data));
        }
        public IActionResult CreateErrorResponse(ErrorCodes code, string message = "")
        {
            int httpCode = (int)code / 100 % 1000;
            return new ObjectResult(_apiResponseFactory.CreateErrorResponse(code, message))
            {
                StatusCode = httpCode
            };
        }

        public IActionResult CreateErrorResponse<T>(T code, string message = "")
        {
            throw new NotImplementedException();
        }
    }
    
    public enum ErrorCodes
    {
        // The first two codes represent app (in the case of future microservice architecture, it is better to know which service problem is)
        // The middle three codes can be directly related to the Http code
        // The last two codes are used for more detailed error distinction
        InternalServerError = 1050000,
        NotFound = 1040400,
        BadRequest = 1040000,
        Unauthorized = 1040100,
        Forbidden = 1040300,
    }
```

After using DI, you can use it like this:

```c#
private readonly IResponseFactory _responseFactory;
public AnyController(
  IResponseFactory responseFactory,
)
{
  _responseFactory = responseFactory;
}

[HttpGet]
public async Task<IActionResult> AnyAction()
{
  // ... Some implementations
  return _responseFactory.CreateOKResponse(result);
}
```
