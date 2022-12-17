using Mapster;
using Microsoft.AspNetCore.Mvc;
using StoreManagementSystem.Data.Concrete;
using StoreManagementSystem.Models;
using StoreManagementSystem.ModelServices;
using StoreManagementSystem.ViewModels.Product;

namespace StoreManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductRepository _productRepository;
    private readonly AppService _appService;

    public ProductController(ProductRepository productRepository,
        AppService appService)
    {
        _productRepository = productRepository;
        _appService = appService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _productRepository.GetAllAsync();

        var entityViewModels = entities.Adapt<IList<ProductViewModel>>();

        foreach (var entityViewModel in entityViewModels) await _appService.SetProductIncludesAsync(entityViewModel);

        return Ok(entityViewModels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _productRepository.GetAsync(id);

        var entityViewModel = entity.Adapt<ProductViewModel>();

        await _appService.SetProductIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Product>();

        var insertedEntity = await _productRepository.InsertAsync(entity);

        var entityViewModel = insertedEntity.Adapt<ProductViewModel>();

        return Ok(entityViewModel);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, ProductUpdateViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Product>();

        entity.Id = id;

        var updatedEntity = await _productRepository.UpdateAsync(id, entity);

        var entityViewModel = updatedEntity.Adapt<ProductViewModel>();

        await _appService.SetProductIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _appService.DeleteProductByIdAsync(id);

        return Ok();
    }
}