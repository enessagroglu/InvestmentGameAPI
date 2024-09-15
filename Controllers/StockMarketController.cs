using InvestmentGameAPI.Application.Interfaces;
using InvestmentGameAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockMarketController : ControllerBase
    {
        private readonly IStockMarketService _stockMarketService;

        public StockMarketController(IStockMarketService stockMarketService)
        {
            _stockMarketService = stockMarketService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartMarket()
        {
            await _stockMarketService.StartMarketAsync();
            return Ok("Market started.");
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateStockPrices()
        {
            await _stockMarketService.UpdateStockPricesAsync();
            return Ok("Stock prices updated.");
        }

        [HttpPost("news/{companyId}")]
        public async Task<IActionResult> ApplyMarketNews(int companyId, [FromBody] string news)
        {
            await _stockMarketService.ApplyMarketNewsAsync(companyId, news);
            return Ok($"Market news applied to Company {companyId}.");
        }

        [HttpGet("companies")]
        public async Task<ActionResult<IEnumerable<Company>>> GetAllCompanies()
        {
            var companies = await _stockMarketService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpGet("company/{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _stockMarketService.GetCompanyAsync(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }

        [HttpGet("company/{id}/daily-change")]
        public async Task<ActionResult<decimal>> GetDailyChangePercentage(int id)
        {
            var change = await _stockMarketService.GetDailyChangePercentageAsync(id);
            return Ok(change);
        }

        [HttpGet("company/{id}/monthly-change")]
        public async Task<ActionResult<decimal>> GetMonthlyChangePercentage(int id)
        {
            var change = await _stockMarketService.GetMonthlyChangePercentageAsync(id);
            return Ok(change);
        }

        [HttpGet("company/{id}/yearly-change")]
        public async Task<ActionResult<decimal>> GetYearlyChangePercentage(int id)
        {
            var change = await _stockMarketService.GetYearlyChangePercentageAsync(id);
            return Ok(change);
        }

        [HttpGet("company/{id}/price-history")]
        public async Task<ActionResult<IEnumerable<CompanyPriceHistory>>> GetCompanyPriceHistory(int id)
        {
            var history = await _stockMarketService.GetCompanyPriceHistoryAsync(id);
            return Ok(history);
        }

        [HttpPost("generate-fake-data")]
        public async Task<IActionResult> GenerateFakeDataForOneYear()
        {
            await _stockMarketService.GenerateFakeDataForOneYearAsync();
            return Ok("Fake data for one year generated.");
        }

        [HttpPost("set-bull-market")]
        public IActionResult SetBullMarket()
        {
            _stockMarketService.SetBullMarket();
            return Ok("Market set to Bull.");
        }

        [HttpPost("set-bear-market")]
        public IActionResult SetBearMarket()
        {
            _stockMarketService.SetBearMarket();
            return Ok("Market set to Bear.");
        }

        [HttpPost("set-normal-market")]
        public IActionResult SetNormalMarket()
        {
            _stockMarketService.SetNormalMarket();
            return Ok("Market set to Normal.");
        }



    }
}
