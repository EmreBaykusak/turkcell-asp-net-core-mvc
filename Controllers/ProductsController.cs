using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Controllers;

[Route("[controller]/[action]")]
public class ProductsController : Controller
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    private readonly IFileProvider _fileProvider;
    
    public ProductsController(AppDbContext context, IMapper mapper, IFileProvider fileProvider)
    {
        _context = context;
        _mapper = mapper;
        _fileProvider = fileProvider;

        _context.SaveChanges();
    }
    // [CacheResourceFilter]
    public IActionResult Index()
    {
        List<ProductViewModel> products = _context.Products.Include(x => x.Category).Select(x => new ProductViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            Stock = x.Stock,
            CategoryName = x.Category.Name,
            Color = x.Color,
            Description = x.Description,
            Expire = x.Expire,
            ImagePath = x.ImagePath,
            IsPublish = x.IsPublish,
            PublishDate = x.PublishDate

        }).ToList();
        
        return View(products);
    }
    [Route("[controller]/[action]/{productId}", Name = "product")]
    [ServiceFilter(typeof(NotFoundFilter))]
    public IActionResult GetById(int productId)
    {
        var product = _context.Products.Find(productId);

        return View(_mapper.Map<ProductViewModel>(product));
    }
    [Route("[controller]/[action]/{page}/{pageSize}",Name = "productPage")]
    public IActionResult Pages(int page, int pageSize)
    {
        var products = _context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        
        ViewBag.page = page;
        ViewBag.pageSize = pageSize;
        
        return View(_mapper.Map<List<ProductViewModel>>(products));
    }
    [ServiceFilter(typeof(NotFoundFilter))]
    public IActionResult Remove(int id)
    {
        var product = _context.Products.Find(id);
        _context.Products.Remove(product);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    public IActionResult Add()
    {
        ViewBag.Expire = new Dictionary<string, int>()
        {
            { "1 Month", 1},
            { "3 Month", 3},
            { "6 Month", 6},
            { "12 Month", 12}
        };

        ViewBag.Color = new SelectList(new List<ColorSelectList>
        {
          new() {Data = "Black", Value = "Black"},  
          new() {Data = "Gray", Value = "Gray"}, 
          new() {Data = "White", Value = "White"}  
        },"Value", "Data");

        var categories = _context.Category.ToList();

        ViewBag.categorySelect = new SelectList(categories, "Id", "Name");

        return View();
    }
    [HttpPost]
    public IActionResult Add(ProductViewModel newProduct)
    {
        IActionResult result = null;
        
        if (ModelState.IsValid)
        {
            try
            {
                var product = _mapper.Map<Product>(newProduct);

                if (newProduct.Image is { Length: > 0 })
                {
                    var root = _fileProvider.GetDirectoryContents("wwwroot");
                    var images = root.First(x => x.Name == "images");
                    var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName);
                    var path = Path.Combine(images.PhysicalPath, randomImageName);
                    using var stream = new FileStream(path, FileMode.Create);
                    newProduct.Image.CopyTo(stream);
                
                    product.ImagePath = randomImageName;
                }
                
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["status"] = "Product has been successfully added";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                // ModelState.AddModelError("error", new ErrorViewModel()
                // {
                //     Errors = new List<string>(){"Problem occurred when saving data, please try again"}
                // }.ToString());
                result = View();
            }
        }
        else
        {
            result = View();
        }

        var categories = _context.Category.ToList();
        ViewBag.categorySelect = new SelectList(categories,"Id", "Name");

        ViewBag.Expire = new Dictionary<string, int>()
        {
            { "1 Month", 1},
            { "3 Month", 3},
            { "6 Month", 6},
            { "12 Month", 12}
        };

        ViewBag.Color = new SelectList(new List<ColorSelectList>
        {
            new() {Data = "Black", Value = "Black"},  
            new() {Data = "Gray", Value = "Gray"}, 
            new() {Data = "White", Value = "White"}  
        },"Value", "Data");

        return result;
    }
    [ServiceFilter(typeof(NotFoundFilter))]
    public IActionResult Update(int id)
    {
        var product = _context.Products.Find(id);
        var categories = _context.Category.ToList();
        
        ViewBag.categorySelect = new SelectList(categories,"Id", "Name",product.CategoryId);


        ViewBag.ExpireValue = product.Expire;
        
        ViewBag.Expire = new Dictionary<string, int>()
        {
            { "1 Month", 1},
            { "3 Month", 3},
            { "6 Month", 6},
            { "12 Month", 12}
        };

        ViewBag.Color = new SelectList(new List<ColorSelectList>
        {
            new() {Data = "Black", Value = "Black"},  
            new() {Data = "Gray", Value = "Gray"}, 
            new() {Data = "White", Value = "White"}  
        },"Value", "Data", product.Color);
        
        return View(_mapper.Map<ProductUpdateViewModel>(product));
    }
    [HttpPost]
    public IActionResult Update(ProductUpdateViewModel updatedProduct)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ExpireValue = updatedProduct.Expire;
        
            ViewBag.Expire = new Dictionary<string, int>()
            {
                { "1 Month", 1},
                { "3 Month", 3},
                { "6 Month", 6},
                { "12 Month", 12}
            };

            ViewBag.Color = new SelectList(new List<ColorSelectList>
            {
                new() {Data = "Black", Value = "Black"},  
                new() {Data = "Gray", Value = "Gray"}, 
                new() {Data = "White", Value = "White"}  
            },"Value", "Data", updatedProduct.Color);
            
            var categories = _context.Category.ToList();
            
            ViewBag.categorySelect = new SelectList(categories,"Id", "Name",updatedProduct.CategoryId);

            return View();
        }
        if (updatedProduct.Image is { Length: > 0 })
        {
            var root = _fileProvider.GetDirectoryContents("wwwroot");
            var images = root.First(x => x.Name == "images");
            var randomImageName = Guid.NewGuid() + Path.GetExtension(updatedProduct.Image.FileName);
            var path = Path.Combine(images.PhysicalPath, randomImageName);
            using var stream = new FileStream(path, FileMode.Create);
            updatedProduct.Image.CopyTo(stream);
                
            updatedProduct.ImagePath = randomImageName;
        }
        _context.Products.Update(_mapper.Map<Product>(updatedProduct));
        _context.SaveChanges();
        TempData["status"] = "Product has been successfully updated";
        return RedirectToAction("Index");
    }
    [AcceptVerbs("GET", "POST")]
    public IActionResult HasProductName(string name)
    {
        var isProductNameExists = _context.Products.Any(x => x.Name.ToLower() == name.ToLower());

        if (isProductNameExists)
        {
            return Json($"The product with the name {name} has exists. Please try different name for the product.");
        }

        return Json(true);
    }
}