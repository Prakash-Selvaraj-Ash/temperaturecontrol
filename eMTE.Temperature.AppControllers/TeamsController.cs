using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Temperature.BusinessLayer.DTO.Team.Request;
using eMTE.Temperature.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eMTE.Temperature.AppControllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;
        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateTeams(CreateTeam createTeam, CancellationToken cancellationToken = default)
        {
            var claims = HttpContext.User.Claims;
            await _teamService.CreateAsync(
                Guid.Parse(claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                Guid.Parse(claims.Single(claim => claim.Type == "OrganizationId").Value),
                createTeam,
                cancellationToken);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetManagerTeams(CancellationToken cancellationToken = default)
        {
            var claims = HttpContext.User.Claims;
            var managerId = Guid.Parse(claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            var teams = await _teamService.GetManagerTeams(managerId, cancellationToken);

            return new OkObjectResult(teams);
        }
    }
}
