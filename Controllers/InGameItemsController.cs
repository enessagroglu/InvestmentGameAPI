using InvestmentGameAPI.Application.Interfaces;
using InvestmentGameAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InGameItemsController : ControllerBase
    {
        private readonly IInGameItemService _itemService;

        public InGameItemsController(IInGameItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InGameItem>>> GetItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InGameItem>> GetItem(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<InGameItem>> PostItem(InGameItem item)
        {
            var createdItem = await _itemService.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, createdItem);
        }
    }
}
