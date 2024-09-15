using InvestmentGameAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Application.Interfaces
{
    public interface IStockMarketService
    {
        Task StartMarketAsync();
        Task UpdateStockPricesAsync();
        Task ApplyMarketNewsAsync(int companyId, string news);
        Task<Company> GetCompanyAsync(int companyId);
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<decimal> GetDailyChangePercentageAsync(int companyId);
        Task<decimal> GetMonthlyChangePercentageAsync(int companyId);
        Task<decimal> GetYearlyChangePercentageAsync(int companyId);
        Task<IEnumerable<CompanyPriceHistory>> GetCompanyPriceHistoryAsync(int companyId);
        Task GenerateFakeDataForOneYearAsync();
        void SetBullMarket();
        void SetBearMarket();
        void SetNormalMarket();

    }
}
