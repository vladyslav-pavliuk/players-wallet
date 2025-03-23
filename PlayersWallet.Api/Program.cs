using PlayersWallet.Api.Repositories;
using PlayersWallet.Api.Repositories.Interfaces;
using PlayersWallet.Api.Services;
using PlayersWallet.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Register repositories
builder.Services.AddSingleton<IPlayerRepository, PlayerRepository>();
builder.Services.AddSingleton<IWalletRepository, WalletRepository>();
builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();

// Register services
builder.Services.AddSingleton<IWalletService, WalletService>();

// Add Redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "WalletService_";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();