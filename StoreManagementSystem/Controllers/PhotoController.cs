using Mapster;
using Microsoft.AspNetCore.Mvc;
using StoreManagementSystem.Data.Concrete;
using StoreManagementSystem.Models;
using StoreManagementSystem.ModelServices;
using StoreManagementSystem.ViewModels.Photo;

namespace StoreManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoController : ControllerBase
{
    private readonly PhotoRepository _photoRepository;
    private readonly AppService _appService;

    public PhotoController(PhotoRepository photoRepository,
        AppService appService)
    {
        _photoRepository = photoRepository;
        _appService = appService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _photoRepository.GetAllAsync();

        var entityViewModels = entities.Adapt<IList<PhotoViewModel>>();

        foreach (var entityViewModel in entityViewModels) await _appService.SetPhotoIncludesAsync(entityViewModel);

        return Ok(entityViewModels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _photoRepository.GetAsync(id);

        var entityViewModel = entity.Adapt<PhotoViewModel>();

        await _appService.SetPhotoIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Post(PhotoAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Photo>();

        var insertedEntity = await _photoRepository.InsertAsync(entity);

        var entityViewModel = insertedEntity.Adapt<PhotoViewModel>();

        return Ok(entityViewModel);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, PhotoUpdateViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Photo>();

        entity.Id = id;

        var updatedEntity = await _photoRepository.UpdateAsync(id, entity);

        var entityViewModel = updatedEntity.Adapt<PhotoViewModel>();

        await _appService.SetPhotoIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _appService.DeletePhotoByIdAsync(id);

        return Ok();
    }
}