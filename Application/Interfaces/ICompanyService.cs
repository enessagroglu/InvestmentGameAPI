using InvestmentGameAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(int id); 
        Task<Company> CreateCompanyAsync(Company company);
    }
}
