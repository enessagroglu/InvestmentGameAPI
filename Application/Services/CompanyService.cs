using InvestmentGameAPI.Application.Interfaces;
using InvestmentGameAPI.Infrastructure.Data;
using InvestmentGameAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly InvestmentDbContext _context;

        public CompanyService(InvestmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id)!;  
        }



        public async Task<Company> CreateCompanyAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }
    }
}
