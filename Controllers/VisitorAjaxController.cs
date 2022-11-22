using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Controllers;

[Route("[controller]/[action]")]
public class VisitorAjaxController : Controller
{
    private readonly AppDbContext _context;

    private readonly IMapper _mapper;

    // GET
    public VisitorAjaxController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
    {
        var visitor = _mapper.Map<Visitor>(visitorViewModel);
        visitor.Created = DateTime.Now;
        _context.Visitors.Add(visitor);
        _context.SaveChanges();

        return Json(new { IsSuccess = true });
    }

    [HttpGet]
    public IActionResult VisitorCommentList()
    {
        var visitors = _context.Visitors.OrderByDescending(x => x.Created).ToList();
        var visitorsViewModels = _mapper.Map<List<VisitorViewModel>>(visitors);

        return Json(visitorsViewModels);
    }
}