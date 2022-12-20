using Mapster;
using Microsoft.AspNetCore.Mvc;
using StoreManagementSystem.Data.Concrete;
using StoreManagementSystem.Models;
using StoreManagementSystem.ModelServices;
using StoreManagementSystem.ViewModels.Purchase;

namespace StoreManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchasesController : ControllerBase
{
    private readonly PurchaseRepository _purchaseRepository;
    private readonly AppService _appService;

    public PurchasesController(PurchaseRepository purchaseRepository,
        AppService appService)
    {
        _purchaseRepository = purchaseRepository;
        _appService = appService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _purchaseRepository.GetAllAsync();

        var entityViewModels = entities.Adapt<IList<PurchaseViewModel>>();

        foreach (var entityViewModel in entityViewModels) await _appService.SetPurchaseIncludesAsync(entityViewModel);

        return Ok(entityViewModels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _purchaseRepository.GetAsync(id);

        var entityViewModel = entity.Adapt<PurchaseViewModel>();

        await _appService.SetPurchaseIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Post(PurchaseAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Purchase>();

        var insertedEntity = await _purchaseRepository.InsertAsync(entity);

        var entityViewModel = insertedEntity.Adapt<PurchaseViewModel>();

        return Ok(entityViewModel);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, PurchaseUpdateViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Purchase>();

        entity.Id = id;

        var updatedEntity = await _purchaseRepository.UpdateAsync(id, entity);

        var entityViewModel = updatedEntity.Adapt<PurchaseViewModel>();

        await _appService.SetPurchaseIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _appService.DeletePurchaseByIdAsync(id);

        return Ok();
    }
}