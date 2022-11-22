using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyAspNetCoreApp.Web.ViewModels;

public class ProductUpdateViewModel
{
    public int Id { get; set; }
    [StringLength(50, ErrorMessage = "Name field can not be more than 50 characters")]
    [Required(ErrorMessage = "Name field is required")]
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    [Required(ErrorMessage = "Price field is required")]
    public decimal? Price { get; set; }
    [Required(ErrorMessage = "Stock field is required")]
    [Range(1,200, ErrorMessage = "Please enter stock between 1 to 200")]
    public int? Stock { get; set; }
    [StringLength(300,MinimumLength = 10,ErrorMessage = "Description field should be in between 10 to 300 characters")]
    [Required(ErrorMessage = "Description field is required")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Color field is required")]
    public string? Color { get; set; }
    public bool IsPublish { get; set; }
    [Required(ErrorMessage = "Publish Date field is required")]
    public DateTime? PublishDate { get; set; }
    [Required(ErrorMessage = "Expire field is required")]
    public int? Expire { get; set; }
    [EmailAddress(ErrorMessage = "Email address format is invalid")]
    [ValidateNever]
    public string Email { get; set; }
    [ValidateNever]
    public IFormFile? Image { get; set; }
    [ValidateNever]
    public string ImagePath { get; set; }
}