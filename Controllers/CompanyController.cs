using InvestmentGameAPI.Application.Interfaces;
using InvestmentGameAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // Tüm şirketleri getirme
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        // Belirli bir şirketi id'sine göre getirme
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // Yeni bir şirket ekleme
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            var createdCompany = await _companyService.CreateCompanyAsync(company);
            return CreatedAtAction(nameof(GetCompany), new { id = createdCompany.Id }, createdCompany);
        }
    }
}
