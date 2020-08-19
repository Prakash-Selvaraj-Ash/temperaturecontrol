using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Temperature.BusinessLayer.DTO.Team.Request;
using eMTE.Temperature.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static eMTE.Temperature.BusinessLayer.Constants.Constants;

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
                Guid.Parse(claims.Single(claim => claim.Type == Claims.Organization).Value),
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

        [Authorize]
        [HttpGet("allTeams")]
        public async Task<ActionResult> GetAllTeams(CancellationToken cancellationToken = default)
        {
            var claims = HttpContext.User.Claims;
            var organizationId = Guid.Parse(claims.Single(claim => claim.Type == Claims.Organization).Value);
            var teams = await _teamService.GetAllTeams(organizationId, cancellationToken);

            return new OkObjectResult(teams);
        }

        [Authorize]
        [HttpPut("assign/{teamId}")]
        public async Task<ActionResult> AssignTeam(Guid teamId, CancellationToken cancellationToken = default)
        {
            var claims = HttpContext.User.Claims;
            var userId = Guid.Parse(claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            await _teamService.AssignTeam(userId, teamId, cancellationToken);

            return Ok();
        }

        [Authorize]
        [HttpPut("remove/{userId}")]
        public async Task<IActionResult> RemoveMember(Guid teamId, Guid userId, CancellationToken cancellationToken = default)
        {
            await _teamService.RemoveMember(teamId, userId, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpGet("{teamId}/members")]
        public async Task<IActionResult> GetMembersByTeamId(Guid teamId, CancellationToken cancellationToken = default)
        {
            var members = await _teamService.GetMembers(teamId, cancellationToken);
            return new OkObjectResult(members);
        }

        [AllowAnonymous]
        [HttpGet("getTeam/{teamId}")]
        public async Task<IActionResult> GetTeamData(Guid teamId, CancellationToken cancellationToken = default)
        {
            var team = await _teamService.GetTeamData(teamId, cancellationToken);
            return new OkObjectResult(team);
        }
    }
}
