using Mapster;
using Microsoft.AspNetCore.Mvc;
using StoreManagementSystem.Data.Concrete;
using StoreManagementSystem.Models;
using StoreManagementSystem.ModelServices;
using StoreManagementSystem.ViewModels.Store;

namespace StoreManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoresController : ControllerBase
{
    private readonly StoreRepository _storeRepository;
    private readonly AppService _appService;
    
    public StoresController(StoreRepository storeRepository,
        AppService appService)
    {
        _storeRepository = storeRepository;
        _appService = appService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _storeRepository.GetAllAsync();

        var entityViewModels = entities.Adapt<IList<StoreViewModel>>();

        foreach (var entityViewModel in entityViewModels) await _appService.SetStoreIncludesAsync(entityViewModel);

        return Ok(entityViewModels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _storeRepository.GetAsync(id);

        var entityViewModel = entity.Adapt<StoreViewModel>();

        await _appService.SetStoreIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpPost("{cityId:int}")]
    public async Task<IActionResult> Post(int cityId, StoreAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Store>();

        entity.CityId = cityId;

        var insertedEntity = await _storeRepository.InsertAsync(entity);

        var entityViewModel = insertedEntity.Adapt<StoreViewModel>();

        return Ok(entityViewModel);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, StoreUpdateViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Store>();

        entity.Id = id;

        var updatedEntity = await _storeRepository.UpdateAsync(id, entity);

        var entityViewModel = updatedEntity.Adapt<StoreViewModel>();

        await _appService.SetStoreIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _appService.DeleteStoreByIdAsync(id);

        return Ok();
    }
}