using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers;

[Route("[controller]/[action]")]
public class CookieController : Controller
{
    // GET
    public IActionResult CookieCreate()
    {
        HttpContext.Response.Cookies.Append("background-color", "red", new CookieOptions()
        {
            Expires = DateTime.Now.AddDays(2)
        });
        return View();
    }

    public IActionResult CookieRead()
    {
        var bgColor = HttpContext.Request.Cookies["background-color"];
        ViewBag.bgColor = bgColor;
        return View();
    }
}