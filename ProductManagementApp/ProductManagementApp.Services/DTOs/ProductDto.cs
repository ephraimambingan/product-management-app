using System.ComponentModel.DataAnnotations;

namespace ProductManagementApp.Services.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Product name is required.")]
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public decimal Price { get; set; }
}