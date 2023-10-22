using Microsoft.AspNetCore.Mvc;
using Utilities.Shared;

namespace Utilities.Services;

public interface IErrorHandlerService
{
    JsonResult GetErrorResponse(StandardServiceResult standardServiceResult);
}