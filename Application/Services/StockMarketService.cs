using InvestmentGameAPI.Application.Interfaces;
using InvestmentGameAPI.Infrastructure.Data;
using InvestmentGameAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Application.Services
{
    public class StockMarketService : IStockMarketService
    {
        private readonly InvestmentDbContext _context;
        private Random _random = new Random();
        private static MarketState _currentMarketState = MarketState.Normal;

        public StockMarketService(InvestmentDbContext context)
        {
            _context = context;
        }

        // Borsa simülasyonunu başlatma
        public async Task StartMarketAsync()
        {
            if (!_context.Companies.Any()) // Eğer veri tabanında şirket yoksa
            {
                for (int i = 1; i <= 10; i++)
                {
                    _context.Companies.Add(new Company
                    {
                        Name = $"Company {i}",
                        CurrentStockPrice = _random.Next(100, 500), // Rastgele bir başlangıç fiyatı
                        TotalShares = _random.Next(1000, 10000),
                        MarketCap = _random.Next(100000, 1000000)
                    });
                }

                await _context.SaveChangesAsync();
            }
        }

        // Hisse fiyatlarını güncelleme

        public async Task UpdateStockPricesAsync()
        {
            var companies = await _context.Companies.ToListAsync();
            foreach (var company in companies)
            {
                // Piyasa durumuna göre fiyat güncellemesi yap
                switch (_currentMarketState)
                {
                    case MarketState.Bull:
                        // Boğa piyasasında fiyatlar sürekli artmalı
                        company.CurrentStockPrice *= 1.02m; 
                        break;
                    case MarketState.Bear:
                        // Ayı piyasasında fiyatlar sürekli düşmeli
                        company.CurrentStockPrice *= 0.07m; 
                        break;
                    case MarketState.Normal:
                        // Normal piyasada rastgele dalgalanma
                        var change = _random.NextDouble() * 2 - 1; // -1 ile +1 arasında rastgele bir sayı
                        company.CurrentStockPrice += company.CurrentStockPrice * (decimal)(change / 100); // % değişim
                        break;
                }

                // Fiyat sıfırın altına düşemez
                if (company.CurrentStockPrice < 1)
                    company.CurrentStockPrice = 1;

                // Güncel fiyatı tarihsel tabloya ekle
                _context.CompanyPriceHistories.Add(new CompanyPriceHistory
                {
                    CompanyId = company.Id,
                    Price = company.CurrentStockPrice,
                    Date = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
        }


        public async Task<decimal> GetDailyChangePercentageAsync(int companyId)
        {
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);

            var yesterdayPrice = await _context.CompanyPriceHistories
                .Where(h => h.CompanyId == companyId && h.Date <= yesterday)
                .OrderByDescending(h => h.Date)
                .Select(h => h.Price)
                .FirstOrDefaultAsync();

            var currentPrice = await _context.Companies
                .Where(c => c.Id == companyId)
                .Select(c => c.CurrentStockPrice)
                .FirstOrDefaultAsync();

            return CalculatePercentageChange(yesterdayPrice, currentPrice);
        }

        public async Task<decimal> GetMonthlyChangePercentageAsync(int companyId)
        {
            var today = DateTime.Today;
            var lastMonth = today.AddMonths(-1);

            var lastMonthPrice = await _context.CompanyPriceHistories
                .Where(h => h.CompanyId == companyId && h.Date <= lastMonth)
                .OrderByDescending(h => h.Date)
                .Select(h => h.Price)
                .FirstOrDefaultAsync();

            var currentPrice = await _context.Companies
                .Where(c => c.Id == companyId)
                .Select(c => c.CurrentStockPrice)
                .FirstOrDefaultAsync();

            return CalculatePercentageChange(lastMonthPrice, currentPrice);
        }

        public async Task<decimal> GetYearlyChangePercentageAsync(int companyId)
        {
            var today = DateTime.Today;
            var lastYear = today.AddYears(-1);

            var lastYearPrice = await _context.CompanyPriceHistories
                .Where(h => h.CompanyId == companyId && h.Date <= lastYear)
                .OrderByDescending(h => h.Date)
                .Select(h => h.Price)
                .FirstOrDefaultAsync();

            var currentPrice = await _context.Companies
                .Where(c => c.Id == companyId)
                .Select(c => c.CurrentStockPrice)
                .FirstOrDefaultAsync();

            return CalculatePercentageChange(lastYearPrice, currentPrice);
        }

        public async Task<IEnumerable<CompanyPriceHistory>> GetCompanyPriceHistoryAsync(int companyId)
        {
            return await _context.CompanyPriceHistories
                .Where(h => h.CompanyId == companyId)
                .OrderBy(h => h.Date)
                .ToListAsync();
        }

        private decimal CalculatePercentageChange(decimal oldPrice, decimal newPrice)
        {
            if (oldPrice == 0)
                return 0;

            return ((newPrice - oldPrice) / oldPrice) * 100;
        }

        public async Task GenerateFakeDataForOneYearAsync()
        {
            var companies = await _context.Companies.ToListAsync();
            var random = new Random();
            var startDate = DateTime.Today.AddYears(-1);

            foreach (var company in companies)
            {
                decimal currentPrice = company.CurrentStockPrice;

                for (int i = 0; i < 365; i++)
                {
                    // Fiyat değişimini belirle (örneğin, +/- %1 ile %5 arasında rastgele)
                    var changePercent = random.NextDouble() * (5 - 1) + 1; // %1 ile %5 arasında
                    var changeDirection = random.Next(0, 2) == 0 ? -1 : 1; // Değişim yönü: -1 veya 1
                    var changeAmount = (decimal)(changePercent / 100) * changeDirection;

                    // Güncel fiyatı değiştir
                    currentPrice += currentPrice * changeAmount;

                    // Negatif fiyatları önlemek için kontrol
                    if (currentPrice < 1)
                        currentPrice = 1;

                    // Fiyatı tarihsel tabloya ekle
                    _context.CompanyPriceHistories.Add(new CompanyPriceHistory
                    {
                        CompanyId = company.Id,
                        Price = currentPrice,
                        Date = startDate.AddDays(i)
                    });
                }
            }

            await _context.SaveChangesAsync();
        }


        // Haberin etkisini uygulama
        public async Task ApplyMarketNewsAsync(int companyId, string news)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company != null)
            {
                if (news.Contains("negative"))
                {
                    company.CurrentStockPrice *= 0.9m; // Negatif haber %10 düşüş
                }
                else if (news.Contains("positive"))
                {
                    company.CurrentStockPrice *= 1.1m; // Pozitif haber %10 artış
                }
                await _context.SaveChangesAsync();
            }
        }

        // Şirket verilerini alma
        public async Task<Company> GetCompanyAsync(int companyId)
        {
            return await _context.Companies.FindAsync(companyId);
        }

        // Tüm şirketleri alma
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public void SetBullMarket()
        {
            _currentMarketState = MarketState.Bull;
        }

        public void SetBearMarket()
        {
            _currentMarketState = MarketState.Bear;
        }

        public void SetNormalMarket()
        {
            _currentMarketState = MarketState.Normal;
        }



    }
}
