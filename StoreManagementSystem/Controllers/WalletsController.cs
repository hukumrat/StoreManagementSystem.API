using Mapster;
using Microsoft.AspNetCore.Mvc;
using StoreManagementSystem.Data.Concrete;
using StoreManagementSystem.Models;
using StoreManagementSystem.ModelServices;
using StoreManagementSystem.ViewModels.Wallet;

namespace StoreManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletsController : ControllerBase
{
    private readonly WalletRepository _walletRepository;
    private readonly AppService _appService;

    public WalletsController(WalletRepository walletRepository,
        AppService appService)
    {
        _walletRepository = walletRepository;
        _appService = appService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entities = await _walletRepository.GetAllAsync();

        var entityViewModels = entities.Adapt<IList<WalletViewModel>>();

        foreach (var entityViewModel in entityViewModels) await _appService.SetWalletIncludesAsync(entityViewModel);

        return Ok(entityViewModels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _walletRepository.GetAsync(id);

        var entityViewModel = entity.Adapt<WalletViewModel>();

        await _appService.SetWalletIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Post(WalletAddViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Wallet>();

        var insertedEntity = await _walletRepository.InsertAsync(entity);

        var entityViewModel = insertedEntity.Adapt<WalletViewModel>();

        return Ok(entityViewModel);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, WalletUpdateViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var entity = model.Adapt<Wallet>();

        entity.Id = id;

        var updatedEntity = await _walletRepository.UpdateAsync(id, entity);

        var entityViewModel = updatedEntity.Adapt<WalletViewModel>();

        await _appService.SetWalletIncludesAsync(entityViewModel);

        return Ok(entityViewModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _appService.DeleteWalletByIdAsync(id);

        return Ok();
    }
}