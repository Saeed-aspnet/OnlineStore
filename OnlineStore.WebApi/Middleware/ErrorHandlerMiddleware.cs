using Azure.Identity;
using OnlineStore.Application.Wrappers;
using System.Net;
using System.Text.Json;

namespace OnlineStore.WebApi.Middleware;
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new BaseResult<string>
            {
                Success = false
            };

            switch (error)
            {
                case ArgumentNullException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    responseModel.Errors = new List<Error> { new(ErrorCode.NotFound, e.Message) };
                    break;
                case ArgumentException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = new List<Error> { new(ErrorCode.InventoryCountIsZero, e.Message) };
                    break;
                case InvalidOperationException e: 
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = new List<Error> {  new(ErrorCode.DuplicateData, e.Message) };
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.Errors = new List<Error> { new(ErrorCode.GeneralError, error.Message) };
                    break;
            }
            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}