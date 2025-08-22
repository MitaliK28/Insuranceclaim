using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using insuranceclaimproject.Models;
using insuranceclaimproject.Services;
using System.Threading.Tasks;
using insuranceclaimproject.Dtos;
using insuranceclaimproject.Interfaces;

namespace insuranceclaimproject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // All endpoints in this controller require authentication
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitClaim([FromBody] SubmitClaimDto claimDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var submittedClaim = await _claimService.SubmitClaimAsync(claimDto);

            return CreatedAtAction(
                nameof(GetClaimDetails),
                new { claimId = submittedClaim.ClaimId },
                submittedClaim
            );
        }

        [HttpGet("{claimId}")]
        public async Task<IActionResult> GetClaimDetails(int claimId)
        {
            var claim = await _claimService.GetClaimDetailsAsync(claimId);
            if (claim == null)
            {
                return NotFound();
            }
            return Ok(claim);
        }

        [HttpPut("{claimId}/status")]
        [Authorize(Roles = "ADMIN,CLAIM_ADJUSTER")] // Only admins and adjusters can change claim status
        public async Task<IActionResult> UpdateClaimStatus(int claimId, [FromBody] UpdateClaimStatusDto statusDto)
        {
            var success = await _claimService.UpdateClaimStatusAsync(claimId, statusDto.Status);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}