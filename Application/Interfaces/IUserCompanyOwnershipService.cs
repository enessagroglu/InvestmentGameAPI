using InvestmentGameAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Application.Interfaces
{
    public interface IUserCompanyOwnershipService
    {
        Task<IEnumerable<UserCompanyOwnership>> GetAllOwnershipsAsync();
        Task<UserCompanyOwnership?> GetOwnershipByIdAsync(int userId, int companyId);
        Task<UserCompanyOwnership> CreateOwnershipAsync(UserCompanyOwnership ownership);
    }
}
