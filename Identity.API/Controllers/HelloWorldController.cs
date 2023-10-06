using Microsoft.AspNetCore.Mvc;
using Utilities.Shared;

namespace Identity.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class HelloWorldController : BaseController<HelloWorldController>
{
    private readonly ILogger<HelloWorldController> _logger;

    public HelloWorldController(ILogger<HelloWorldController> logger) : base(logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetHelloWorld")]
    public string GetHelloWorld()
    {
        _logger.LogInformation("Hello World!");
        return "Hello World!";
    }
}