using Microsoft.AspNetCore.Mvc;
using ProductManagementApp.Services;
using ProductManagementApp.Services.DTOs;

namespace ProductManagementApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(
        IProductService productService,
        ILogger<ProductsController> logger)
    {
        this.productService = productService;
        _logger = logger;
    }
    
    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Fetching all products");
        
        try
        {
            return Ok(await productService.GetAllProductsAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching all products.");
        }

        return NoContent();
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("Fetching product by ID: {ProductId}", id);
        
        var product = await productService.GetProductByIdAsync(id);
        if (product == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found.", id);
            return NotFound();
        }
        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await productService.AddProductAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto product)
    {
        if (id != product.Id)
        {
            return BadRequest("Product ID mismatch.");
        }

        var existingProduct = await productService.GetProductByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        await productService.UpdateProductAsync(product);
        return NoContent();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingProduct = await productService.GetProductByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        await productService.DeleteProductAsync(id);
        return NoContent();
    }
}