using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyAspNetCoreApp.Web.Filters;

namespace MyAspNetCoreApp.Web.Controllers;
[Route("[controller]/[action]")]
public class Example2Controller : Controller
{
    [CustomExceptionFilter]
    public IActionResult Index()
    {
        throw new SecurityTokenInvalidLifetimeException();
        return View();
    }

    public IActionResult NoLayout()
    {
        return View();
    }
}