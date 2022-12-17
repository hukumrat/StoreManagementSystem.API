using Mapster;
using Microsoft.AspNetCore.Mvc;
using StoreManagementSystem.Data.Concrete;
using StoreManagementSystem.Models;
using StoreManagementSystem.ModelServices;
using StoreManagementSystem.ViewModels.City;

namespace StoreManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly CityRepository _cityRepository;
    private readonly AppService _appService;

    public CitiesController(CityRepository cityRepository,
        AppService appService)
    {
        _cityRepository = cityRepository;
        _appService = appService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _cityRepository.GetAllAsync();

        var entityViewModel = entities.Adapt<IList<CityViewModel>>();

        foreach (var cityViewModel in entityViewModel) await _appService.SetCityIncludesAsync(cityViewModel);

        return Ok(entityViewModel);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _cityRepository.GetAsync(id);

        var entityViewModel = entity.Adapt<CityViewModel>();

        await _appService.SetCityIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CityAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<City>();

        var insertedEntity = await _cityRepository.InsertAsync(entity);

        var entityViewModel = insertedEntity.Adapt<CityViewModel>();

        return Ok(entityViewModel);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, CityUpdateViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<City>();

        entity.Id = id;

        var updatedEntity = await _cityRepository.UpdateAsync(id, entity);

        var entityViewModel = updatedEntity.Adapt<CityViewModel>();

        await _appService.SetCityIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _appService.DeleteCityByIdAsync(id);

        return Ok();
    }
}