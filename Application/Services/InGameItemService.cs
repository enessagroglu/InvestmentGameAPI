using InvestmentGameAPI.Application.Interfaces;
using InvestmentGameAPI.Infrastructure.Data;
using InvestmentGameAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Application.Services
{
    public class InGameItemService : IInGameItemService
    {
        private readonly InvestmentDbContext _context;

        public InGameItemService(InvestmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InGameItem>> GetAllItemsAsync()
        {
            return await _context.InGameItems.ToListAsync();
        }

        public async Task<InGameItem?> GetItemByIdAsync(int id)
        {
            return await _context.InGameItems.FindAsync(id);
        }

        public async Task<InGameItem> CreateItemAsync(InGameItem item)
        {
            _context.InGameItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
