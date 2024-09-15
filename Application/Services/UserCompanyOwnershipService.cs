using InvestmentGameAPI.Application.Interfaces;
using InvestmentGameAPI.Infrastructure.Data;
using InvestmentGameAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Application.Services
{
    public class UserCompanyOwnershipService : IUserCompanyOwnershipService
    {
        private readonly InvestmentDbContext _context;

        public UserCompanyOwnershipService(InvestmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserCompanyOwnership>> GetAllOwnershipsAsync()
        {
            return await _context.UserCompanyOwnerships.ToListAsync();
        }

        public async Task<UserCompanyOwnership?> GetOwnershipByIdAsync(int userId, int companyId)
        {
            return await _context.UserCompanyOwnerships
                .FirstOrDefaultAsync(o => o.UserId == userId && o.CompanyId == companyId);
        }

        public async Task<UserCompanyOwnership> CreateOwnershipAsync(UserCompanyOwnership ownership)
        {
            _context.UserCompanyOwnerships.Add(ownership);
            await _context.SaveChangesAsync();
            return ownership;
        }
    }
}
