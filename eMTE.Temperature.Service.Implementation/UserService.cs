using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Common.Authentication;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.BusinessLayer.DTO.User.Request;
using eMTE.Temperature.BusinessLayer.DTO.User.Response;
using eMTE.Temperature.BusinessLayer.Extensions;
using eMTE.Temperature.DataAccess.Services;
using eMTE.Temperature.Domain;
using eMTE.Temperature.Service.Contracts;
using Microsoft.EntityFrameworkCore;
using static eMTE.Temperature.BusinessLayer.Constants.Constants;

namespace eMTE.Temperature.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IAuthenticator _authenticator;
        private readonly IEntityService _entityService;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<TeamUserMap> _teamUserMapRepository;

        public UserService(
            IRepository<TeamUserMap> teamUserMapRepository,
            IRepository<Team> teamRepository,
            IRepository<User> userRepository,
            IAuthenticator authenticator,
            IEntityService entityService)
        {

            _teamUserMapRepository = teamUserMapRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _authenticator = authenticator;
            _entityService = entityService;
        }

        public async Task CreateUser(CreateUser createUser, CancellationToken cancellationToken)
        {
            var domainUser = createUser.To<User>();

            var authModel = _authenticator.Create(Secret.PasswordKey, domainUser.Password);
            domainUser.Hash = authModel.Hash;

            await _userRepository.CreateAsync(domainUser, cancellationToken);
            await _entityService.SaveAsync(cancellationToken);
        }

        public async Task<UserPrivilegeResponse> GetMyPrivileges(Guid userId, CancellationToken cancellationToken)
        {
            return new UserPrivilegeResponse
            {
                CanCreateTeam = await CanCreateTeam(userId, cancellationToken)
            };
        }

        private async Task<bool> CanCreateTeam(Guid userId, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.ReadByIdAsync(userId, cancellationToken);
            if (currentUser.IsOrganizationAdmin) { return true; }

            var adminTeamMembersAsyncResult =
                from user in _userRepository.Set.Where(u => u.OrganizationId == currentUser.OrganizationId && u.IsOrganizationAdmin)

                join team in _teamRepository.Set
                on user.Id equals team.TeamManagerId

                join teamUserMap in _teamUserMapRepository.Set
                on team.Id equals teamUserMap.TeamId

                select teamUserMap.User.Id;

            var adminTeamMembers = await adminTeamMembersAsyncResult.ToArrayAsync(cancellationToken);

            return adminTeamMembers.Contains(userId);
        }
    }
}
