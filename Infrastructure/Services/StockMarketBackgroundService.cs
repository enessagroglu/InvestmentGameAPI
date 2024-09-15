using InvestmentGameAPI.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Infrastructure.Services
{
    public class StockMarketBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public StockMarketBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var stockMarketService = scope.ServiceProvider.GetRequiredService<IStockMarketService>();
                    await stockMarketService.UpdateStockPricesAsync(); // Hisse fiyatlarını güncelle
                }

                // Belirli bir süre bekle 
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
