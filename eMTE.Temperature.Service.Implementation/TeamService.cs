using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.BusinessLayer.DTO.Team.Request;
using eMTE.Temperature.BusinessLayer.DTO.Team.Response;
using eMTE.Temperature.BusinessLayer.DTO.User.Response;
using eMTE.Temperature.BusinessLayer.Extensions;
using eMTE.Temperature.DataAccess.Services;
using eMTE.Temperature.Domain;
using eMTE.Temperature.Service.Contracts;
using Microsoft.EntityFrameworkCore;

namespace eMTE.Temperature.Service.Implementation
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<TeamUserMap> _teamUserMapRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IEntityService _entityService;
        public TeamService(
            IRepository<TeamUserMap> teamUserMapRepository,
            IRepository<Team> teamRepository,
            IEntityService entityService)
        {
            _teamUserMapRepository = teamUserMapRepository;
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

        public async Task<IEnumerable<GetTeamResponse>> GetAllTeams(Guid organizationId, CancellationToken cancellationToken)
        {
            return await GetTeams(_teamRepository.Set.Where(team => team.OrganizationId == organizationId), cancellationToken);
        }

        public async Task<IEnumerable<GetTeamResponse>> GetManagerTeams(Guid managerId, CancellationToken cancellationToken)
        {
            return await GetTeams(_teamRepository.Set.Where(team => team.TeamManagerId == managerId), cancellationToken);
        }

        public async Task AssignTeam(Guid userId, Guid teamId, CancellationToken cancellationToken)
        {
            var teamMap = new TeamUserMap
            {
                TeamId = teamId,
                UserId = userId
            };

            await _teamUserMapRepository.CreateAsync(teamMap, cancellationToken);
            await _entityService.SaveAsync(cancellationToken);
        }

        public async Task<IEnumerable<GetUserResponse>> GetMembers(Guid teamId, CancellationToken cancellationToken)
        {
            var users = await _teamUserMapRepository.Set
                .Include(map => map.User)
                .Where(map => map.TeamId == teamId)
                .Select(map => map.User)
                .ToArrayAsync(cancellationToken);

            return users.ToList<GetUserResponse>();
        }

        public async Task RemoveMember(Guid teamId, Guid userId, CancellationToken cancellationToken)
        {
            var mapIdToRemove = await _teamUserMapRepository.Set
                .SingleOrDefaultAsync(map => map.TeamId == teamId && map.UserId == userId, cancellationToken);

            if(mapIdToRemove == null) { return; }
            await _teamUserMapRepository.DeleteByIds<TeamUserMap>(new[] { mapIdToRemove.Id }, cancellationToken);
            await _entityService.SaveAsync(cancellationToken);
        }


        private async Task<IEnumerable<GetTeamResponse>> GetTeams(IQueryable<Team> teamQuery, CancellationToken cancellationToken)
        {
            var allTeams =
                await (from team in teamQuery
                       join userMap in _teamUserMapRepository.Set
                       on team.Id equals userMap.TeamId

                       select new
                       {
                           team,
                           userMap
                       }).ToArrayAsync(cancellationToken);

            return allTeams
                .GroupBy(teammap => teammap.team.Id)
                .Select(group =>
                {
                    var team = group.First().team.To<GetTeamResponse>();
                    team.MembersCount = group.Count();
                    return team;
                });
        }
    }
}
