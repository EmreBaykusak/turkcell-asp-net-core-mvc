using MyAspNetCoreApp.Web.Controllers;

namespace MyAspNetCoreApp.Web.Models;

public class ProductRepository
{
    private static List<Product> _products = new List<Product>();

    public List<Product> GetAll() => _products;
    public void Add(Product product) => _products.Add(product);

    public void Remove(int id)
    {
        var hasProduct = _products.FirstOrDefault(x => x.Id == id);
        if (hasProduct == null)
        {
            throw new Exception($"Product with id({id}) does not exist");
        }
        _products.Remove(hasProduct);
    }

    public void Update(Product updateProduct)
    {
        var hasProduct = _products.FirstOrDefault(x => x.Id == updateProduct.Id);
        if (hasProduct == null)
        {
            throw new Exception($"Product with id({updateProduct.Id}) does not exist");
        }

        hasProduct.Name = updateProduct.Name;
        hasProduct.Price = updateProduct.Price;
        hasProduct.Stock = updateProduct.Stock;
    }
}