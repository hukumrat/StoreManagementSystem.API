using Mapster;
using Microsoft.AspNetCore.Mvc;
using StoreManagementSystem.Data.Concrete;
using StoreManagementSystem.Models;
using StoreManagementSystem.ModelServices;
using StoreManagementSystem.ViewModels.Sale;

namespace StoreManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly SaleRepository _saleRepository;
    private readonly AppService _appService;

    public SalesController(SaleRepository saleRepository,
        AppService appService)
    {
        _saleRepository = saleRepository;
        _appService = appService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _saleRepository.GetAllAsync();

        var entityViewModels = entities.Adapt<IList<SaleViewModel>>();

        foreach (var entityViewModel in entityViewModels) await _appService.SetSaleIncludesAsync(entityViewModel);

        return Ok(entityViewModels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _saleRepository.GetAsync(id);

        var entityViewModel = entity.Adapt<SaleViewModel>();

        await _appService.SetSaleIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Post(SaleAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Sale>();

        var insertedEntity = await _saleRepository.InsertAsync(entity);

        var entityViewModel = insertedEntity.Adapt<SaleViewModel>();

        return Ok(entityViewModel);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, SaleUpdateViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Sale>();

        entity.Id = id;

        var updatedEntity = await _saleRepository.UpdateAsync(id, entity);

        var entityViewModel = updatedEntity.Adapt<SaleViewModel>();

        await _appService.SetSaleIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _appService.DeleteSaleByIdAsync(id);

        return Ok();
    }
}