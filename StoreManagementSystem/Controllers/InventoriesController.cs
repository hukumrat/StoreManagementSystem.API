using Mapster;
using Microsoft.AspNetCore.Mvc;
using StoreManagementSystem.Data.Concrete;
using StoreManagementSystem.Models;
using StoreManagementSystem.ModelServices;
using StoreManagementSystem.ViewModels.Inventory;

namespace StoreManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoriesController : ControllerBase
{
    private readonly InventoryRepository _inventoryRepository;
    private readonly AppService _appService;

    public InventoriesController(InventoryRepository inventoryRepository,
        AppService appService)
    {
        _inventoryRepository = inventoryRepository;
        _appService = appService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _inventoryRepository.GetAllAsync();

        var entityViewModels = entities.Adapt<IList<InventoryViewModel>>();

        foreach (var entityViewModel in entityViewModels) await _appService.SetInventoryIncludesAsync(entityViewModel);

        return Ok(entityViewModels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _inventoryRepository.GetAsync(id);

        var entityViewModel = entity.Adapt<InventoryViewModel>();

        await _appService.SetInventoryIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Post(InventoryAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Inventory>();

        var insertedEntity = await _inventoryRepository.InsertAsync(entity);

        var entityViewModel = insertedEntity.Adapt<InventoryViewModel>();

        return Ok(entityViewModel);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, InventoryUpdateViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Inventory>();

        entity.Id = id;

        var updatedEntity = await _inventoryRepository.UpdateAsync(id, entity);

        var entityViewModel = updatedEntity.Adapt<InventoryViewModel>();

        await _appService.SetInventoryIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _appService.DeleteInventoryByIdAsync(id);

        return Ok();
    }
}