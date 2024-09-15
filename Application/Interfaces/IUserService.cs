using InvestmentGameAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
    }
}
