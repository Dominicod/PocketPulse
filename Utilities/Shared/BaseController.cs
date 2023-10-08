using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Utilities.Shared;

public class BaseController<T> : ControllerBase, IBaseController
{
    private readonly ILogger<T> _logger;
    
    public BaseController(
        ILogger<T> logger)
    {
        _logger = logger;
    }
}