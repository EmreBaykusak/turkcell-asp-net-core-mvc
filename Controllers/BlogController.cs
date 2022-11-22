using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Filters;

namespace MyAspNetCoreApp.Web.Controllers;

[Route("[controller]/[action]")]
public class BlogController : Controller
{
    [CustomResultFilter("x-version", "1.0")]
    [HttpGet("{name}/{id}")]
    public IActionResult Article(string name, int id)
    {
        var routes = Request.RouteValues["article"];
        return View();
    }
    
}