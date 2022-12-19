using Mapster;
using StoreManagementSystem.Data.Concrete;
using StoreManagementSystem.ViewModels.Category;
using StoreManagementSystem.ViewModels.City;
using StoreManagementSystem.ViewModels.Inventory;
using StoreManagementSystem.ViewModels.Photo;
using StoreManagementSystem.ViewModels.Product;
using StoreManagementSystem.ViewModels.Purchase;
using StoreManagementSystem.ViewModels.Sale;
using StoreManagementSystem.ViewModels.Store;
using StoreManagementSystem.ViewModels.Wallet;

namespace StoreManagementSystem.ModelServices;

public class AppService
{
    private readonly CityRepository _cityRepository;
    private readonly StoreRepository _storeRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly InventoryRepository _inventoryRepository;
    private readonly PhotoRepository _photoRepository;
    private readonly ProductRepository _productRepository;
    private readonly PurchaseRepository _purchaseRepository;
    private readonly SaleRepository _saleRepository;
    private readonly WalletRepository _walletRepository;

    public AppService(CityRepository cityRepository,
        StoreRepository storeRepository,
        CategoryRepository categoryRepository,
        InventoryRepository inventoryRepository,
        PhotoRepository photoRepository,
        ProductRepository productRepository,
        PurchaseRepository purchaseRepository,
        SaleRepository saleRepository,
        WalletRepository walletRepository)
    {
        _cityRepository = cityRepository;
        _storeRepository = storeRepository;
        _categoryRepository = categoryRepository;
        _inventoryRepository = inventoryRepository;
        _photoRepository = photoRepository;
        _productRepository = productRepository;
        _purchaseRepository = purchaseRepository;
        _saleRepository = saleRepository;
        _walletRepository = walletRepository;
    }

    public async Task SetCityIncludesAsync(CityViewModel viewModel)
    {
        var stores = await _storeRepository.GetAllByCityId(viewModel.Id);

        var storeViewModels = stores.Adapt<IList<StoreViewModel>>();

        viewModel.Stores = storeViewModels;
    }

    public async Task SetStoreIncludesAsync(StoreViewModel viewModel)
    {
        var categories = await _categoryRepository.GetAllByStoreId(viewModel.Id);
        
        var categoryViewModels = categories.Adapt<IList<CategoryViewModel>>();

        viewModel.Categories = categoryViewModels;
        
        var wallets = await _walletRepository.GetAllByStoreId(viewModel.Id);
        
        var walletViewModels = wallets.Adapt<IList<WalletViewModel>>();

        viewModel.Wallets = walletViewModels;
        
        var inventories = await _inventoryRepository.GetAllByStoreId(viewModel.Id);
        
        var inventoryViewModels = inventories.Adapt<IList<InventoryViewModel>>();

        viewModel.Inventories = inventoryViewModels;
    }
    
    public async Task SetCategoryIncludesAsync(CategoryViewModel viewModel)
    {
        var store = await _storeRepository.GetAsync(viewModel.StoreId);

        var storeViewModel = store.Adapt<StoreViewModel>();

        viewModel.Store = storeViewModel;
        
        var products = await _productRepository.GetAllByCategoryId(viewModel.Id);
        
        var productsViewModels = products.Adapt<IList<ProductViewModel>>();

        viewModel.Products = productsViewModels;
    }
    
    public async Task SetInventoryIncludesAsync(InventoryViewModel viewModel)
    {
        var store = await _storeRepository.GetAsync(viewModel.StoreId);

        var storeViewModel = store.Adapt<StoreViewModel>();

        viewModel.Store = storeViewModel;
        
        var product = await _productRepository.GetAsync(viewModel.ProductId);

        var productViewModel = product.Adapt<ProductViewModel>();

        viewModel.Product = productViewModel;
    }
    
    public async Task SetPhotoIncludesAsync(PhotoViewModel viewModel)
    {
        var product = await _productRepository.GetAsync(viewModel.ProductId);

        var productViewModel = product.Adapt<ProductViewModel>();

        viewModel.Product = productViewModel;
    }

    public async Task SetProductIncludesAsync(ProductViewModel viewModel)
    {
        var category = await _categoryRepository.GetAsync(viewModel.CategoryId);

        var categoryViewModel = category.Adapt<CategoryViewModel>();

        viewModel.Category = categoryViewModel;

        var photos = await _photoRepository.GetAllByProductIdAsync(viewModel.Id);

        var photoViewModels = photos.Adapt<IList<PhotoViewModel>>();

        viewModel.Photos = photoViewModels;
        
        var purchases = await _purchaseRepository.GetAllByProductIdAsync(viewModel.Id);

        var purchaseViewModels = purchases.Adapt<IList<PurchaseViewModel>>();

        viewModel.Purchases = purchaseViewModels;
        
        var sales = await _saleRepository.GetAllByProductIdAsync(viewModel.Id);

        var saleViewModels = sales.Adapt<IList<SaleViewModel>>();

        viewModel.Sales = saleViewModels;
        
        var inventories = await _inventoryRepository.GetAllByProductIdAsync(viewModel.Id);

        var inventoryViewModels = inventories.Adapt<IList<InventoryViewModel>>();

        viewModel.Inventories = inventoryViewModels;
    }
    
    public async Task SetPurchaseIncludesAsync(PurchaseViewModel viewModel)
    {
        var product = await _productRepository.GetAsync(viewModel.ProductId);

        var productViewModel = product.Adapt<ProductViewModel>();

        viewModel.Product = productViewModel;
    }
    
    public async Task SetSaleIncludesAsync(SaleViewModel viewModel)
    {
        var product = await _productRepository.GetAsync(viewModel.ProductId);

        var productViewModel = product.Adapt<ProductViewModel>();

        viewModel.Product = productViewModel;
    }
    
    public async Task SetWalletIncludesAsync(WalletViewModel viewModel)
    {
        var store = await _storeRepository.GetAsync(viewModel.StoreId);

        var storeViewModel = store.Adapt<StoreViewModel>();

        viewModel.Store = storeViewModel;
    }
    
    public async Task DeleteCityByIdAsync(int id)
    {
        var stores = await _storeRepository.GetAllByCityId(id);

        foreach (var store in stores) await DeleteStoreByIdAsync(store.Id);
        
        await _cityRepository.DeleteRowAsync(id);
    }
    
    public async Task DeleteStoreByIdAsync(int id)
    {
        var categories = await _categoryRepository.GetAllByStoreId(id);

        foreach (var category in categories) await DeleteCategoryByIdAsync(category.Id);
        
        var wallets = await _walletRepository.GetAllByStoreId(id);

        foreach (var wallet in wallets) await DeleteWalletByIdAsync(wallet.Id);
        
        var inventories = await _categoryRepository.GetAllByStoreId(id);

        foreach (var inventory in inventories) await DeleteInventoryByIdAsync(inventory.Id);

        await _storeRepository.DeleteRowAsync(id);
    }

    public async Task DeleteCategoryByIdAsync(int id)
    {
        var products = await _productRepository.GetAllByCategoryId(id);
        
        foreach (var product in products) await DeleteProductByIdAsync(product.Id);
        
        await _categoryRepository.DeleteRowAsync(id);
    }

    public async Task DeleteWalletByIdAsync(int id) => await _walletRepository.DeleteRowAsync(id);
    
    public async Task DeleteInventoryByIdAsync(int id) => await _inventoryRepository.DeleteRowAsync(id);
    
    public async Task DeleteProductByIdAsync(int id)
    {
        var photos = await _photoRepository.GetAllByProductIdAsync(id);
        
        foreach (var photo in photos) await DeletePhotoByIdAsync(photo.Id);
        
        var purchases = await _purchaseRepository.GetAllByProductIdAsync(id);
        
        foreach (var purchase in purchases) await DeletePurchaseByIdAsync(purchase.Id);
        
        var sales = await _purchaseRepository.GetAllByProductIdAsync(id);
        
        foreach (var sale in sales) await DeleteSaleByIdAsync(sale.Id);
        
        var inventories = await _inventoryRepository.GetAllByProductIdAsync(id);
        
        foreach (var inventory in inventories) await DeleteInventoryByIdAsync(inventory.Id);
        
        await _productRepository.DeleteRowAsync(id);
    }

    public async Task DeletePhotoByIdAsync(int id) => await _photoRepository.DeleteRowAsync(id);
    
    public async Task DeletePurchaseByIdAsync(int id) => await _purchaseRepository.DeleteRowAsync(id);
    
    public async Task DeleteSaleByIdAsync(int id) => await _saleRepository.DeleteRowAsync(id);
}