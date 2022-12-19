using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using StoreManagementSystem.Data.Abstract;
using StoreManagementSystem.Data.Concrete;
using StoreManagementSystem.ModelServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CategoryRepository>(x => new CategoryRepository("Categories"));
builder.Services.AddScoped<CityRepository>(x => new CityRepository("Cities"));
builder.Services.AddScoped<InventoryRepository>(x => new InventoryRepository("Inventories"));
builder.Services.AddScoped<PhotoRepository>(x => new PhotoRepository("Photos"));
builder.Services.AddScoped<ProductRepository>(x => new ProductRepository("Products"));
builder.Services.AddScoped<PurchaseRepository>(x => new PurchaseRepository("Purchases"));
builder.Services.AddScoped<SaleRepository>(x => new SaleRepository("Sales"));
builder.Services.AddScoped<StoreRepository>(x => new StoreRepository("Stores"));
builder.Services.AddScoped<WalletRepository>(x => new WalletRepository("Wallets"));
builder.Services.AddScoped<AppService>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.EnableTryItOutByDefault();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
