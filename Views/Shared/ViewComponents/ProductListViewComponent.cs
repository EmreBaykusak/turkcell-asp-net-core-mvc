using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Views.Shared.ViewComponents;
// [ViewComponent(Name = "p-list")] Changes the tag helper name of the view component
public class ProductListViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public ProductListViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(int type=1)
    {
        var viewModels = _context.Products.Select(x => new ProductListComponentViewModel()
        {
            Name = x.Name,
            Description = x.Description
        }).ToList();

        return View(type == 1 ? "Default" : "Type2", viewModels);
    }
}