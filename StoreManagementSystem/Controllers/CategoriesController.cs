using Mapster;
using Microsoft.AspNetCore.Mvc;
using StoreManagementSystem.Data.Concrete;
using StoreManagementSystem.Models;
using StoreManagementSystem.ModelServices;
using StoreManagementSystem.ViewModels.Category;
using StoreManagementSystem.ViewModels.Store;

namespace StoreManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CategoryRepository _categoryRepository;
    private readonly AppService _appService;
    
    public CategoriesController(CategoryRepository categoryRepository, 
        AppService appService)
    {
        _categoryRepository = categoryRepository;
        _appService = appService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _categoryRepository.GetAllAsync();

        var entityViewModels = entities.Adapt<IList<CategoryViewModel>>();

        foreach (var entityViewModel in entityViewModels) await _appService.SetCategoryIncludesAsync(entityViewModel);

        return Ok(entityViewModels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _categoryRepository.GetAsync(id);

        var entityViewModel = entity.Adapt<CategoryViewModel>();

        await _appService.SetCategoryIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }
    
    [HttpGet("GetAllWithStoreAndCity")]
    public async Task<IActionResult> GetAllWithStoreAndCity()
    {
        var entityViewModels = await _categoryRepository.GetAllWithStoreAndCity();

        return Ok(entityViewModels);
    }

    [HttpPost("{storeId:int}")]
    public async Task<IActionResult> Post(int storeId, CategoryAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Category>();

        entity.StoreId = storeId;

        var insertedEntity = await _categoryRepository.InsertAsync(entity);

        var entityViewModel = insertedEntity.Adapt<CategoryViewModel>();

        return Ok(entityViewModel);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, CategoryUpdateViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Category>();

        entity.Id = id;

        var updatedEntity = await _categoryRepository.UpdateAsync(id, entity);

        var entityViewModel = updatedEntity.Adapt<CategoryViewModel>();

        await _appService.SetCategoryIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _appService.DeleteCategoryByIdAsync(id);

        return Ok();
    }
}