using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Temperature.BusinessLayer.DTO.Team.Request;
using eMTE.Temperature.BusinessLayer.DTO.Team.Response;
using eMTE.Temperature.BusinessLayer.DTO.User.Response;

namespace eMTE.Temperature.Service.Contracts
{
    public interface ITeamService
    {
        Task CreateAsync(Guid managerId, Guid organizationId, CreateTeam createTeam, CancellationToken cancellationToken);
        Task<IEnumerable<GetTeamResponse>> GetManagerTeams(Guid managerId, CancellationToken cancellationToken);
        Task<IEnumerable<GetTeamResponse>> GetAllTeams(Guid organizationId, CancellationToken cancellationToken);
        Task AssignTeam(Guid userId, Guid teamId, CancellationToken cancellationToken);
        Task<IEnumerable<GetUserResponse>> GetMembers(Guid teamId, CancellationToken cancellationToken);
        Task RemoveMember(Guid teamId, Guid userId, CancellationToken cancellationToken);
    }
}
