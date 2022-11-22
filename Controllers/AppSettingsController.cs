using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers;
[Route("[controller]/[action]")]
public class AppSettingsController : Controller
{
    private readonly IConfiguration _configuration;
    // GET
    public AppSettingsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        ViewBag.baseUrl = _configuration["BaseUrl"];
        ViewBag.smsKey = _configuration["Keys:SMS"];
        ViewBag.emailKey = _configuration.GetSection("Keys")["Email"];
        
        return View();
    }
}