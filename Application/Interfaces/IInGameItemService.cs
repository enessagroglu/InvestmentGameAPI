using InvestmentGameAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Application.Interfaces
{
    public interface IInGameItemService
    {
        Task<IEnumerable<InGameItem>> GetAllItemsAsync();
        Task<InGameItem?> GetItemByIdAsync(int id);
        Task<InGameItem> CreateItemAsync(InGameItem item);
    }
}
