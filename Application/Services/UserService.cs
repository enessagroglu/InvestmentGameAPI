using InvestmentGameAPI.Application.Interfaces;
using InvestmentGameAPI.Infrastructure.Data;
using InvestmentGameAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Application.Services
{
    public class UserService : IUserService
    {
        private readonly InvestmentDbContext _context;

        public UserService(InvestmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
