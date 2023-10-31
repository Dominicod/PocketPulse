using Microsoft.AspNetCore.Mvc;
using Utilities.Shared;

namespace Utilities.Services;

public interface IResponseHandlerService
{
    JsonResult GetErrorResponse(StandardServiceResult standardServiceResult);
    JsonResult GetErrorResponse(Exception exception);
    JsonResult GetOkResponse(object data);
    CreatedResult GetCreatedResponse();
}