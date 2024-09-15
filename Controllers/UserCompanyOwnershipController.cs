using InvestmentGameAPI.Application.Interfaces;
using InvestmentGameAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentGameAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCompanyOwnershipsController : ControllerBase
    {
        private readonly IUserCompanyOwnershipService _ownershipService;

        public UserCompanyOwnershipsController(IUserCompanyOwnershipService ownershipService)
        {
            _ownershipService = ownershipService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCompanyOwnership>>> GetOwnerships()
        {
            var ownerships = await _ownershipService.GetAllOwnershipsAsync();
            return Ok(ownerships);
        }

        [HttpGet("{userId}/{companyId}")]
        public async Task<ActionResult<UserCompanyOwnership>> GetOwnership(int userId, int companyId)
        {
            var ownership = await _ownershipService.GetOwnershipByIdAsync(userId, companyId);

            if (ownership == null)
            {
                return NotFound();
            }

            return Ok(ownership);
        }

        [HttpPost]
        public async Task<ActionResult<UserCompanyOwnership>> PostOwnership(UserCompanyOwnership ownership)
        {
            var createdOwnership = await _ownershipService.CreateOwnershipAsync(ownership);
            return CreatedAtAction(nameof(GetOwnership), new { userId = createdOwnership.UserId, companyId = createdOwnership.CompanyId }, createdOwnership);
        }
    }
}
