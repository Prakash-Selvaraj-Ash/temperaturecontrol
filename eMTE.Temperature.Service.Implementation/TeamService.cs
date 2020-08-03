using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.BusinessLayer.DTO.Team.Request;
using eMTE.Temperature.BusinessLayer.DTO.Team.Response;
using eMTE.Temperature.BusinessLayer.Extensions;
using eMTE.Temperature.DataAccess.Services;
using eMTE.Temperature.Domain;
using eMTE.Temperature.Service.Contracts;
using Microsoft.EntityFrameworkCore;

namespace eMTE.Temperature.Service.Implementation
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<Team> _teamRepository;
        private readonly IEntityService _entityService;
        public TeamService(
            IRepository<Team> teamRepository,
            IEntityService entityService)
        {
            _teamRepository = teamRepository;
            _entityService = entityService;
        }

        public async Task CreateAsync(Guid managerId, Guid organizationId, CreateTeam createTeam, CancellationToken cancellationToken)
        {
            var teamDomain = createTeam.To<Team>();

            teamDomain.OrganizationId = organizationId;
            teamDomain.TeamManagerId = managerId;

            await _teamRepository.CreateAsync(teamDomain, cancellationToken);
            await _entityService.SaveAsync(cancellationToken);
        }

        public async Task<IEnumerable<GetTeamResponse>> GetManagerTeams(Guid managerId, CancellationToken cancellationToken)
        {
            var teams = await _teamRepository.Set.Where(team => team.TeamManagerId == managerId).ToArrayAsync(cancellationToken);
            return teams.ToList<GetTeamResponse>();
        }
    }
}
