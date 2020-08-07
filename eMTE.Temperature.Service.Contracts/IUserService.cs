using System;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Temperature.BusinessLayer.DTO.User.Request;
using eMTE.Temperature.BusinessLayer.DTO.User.Response;

namespace eMTE.Temperature.Service.Contracts
{
    public interface IUserService
    {
        Task CreateUser(CreateUser createUser, CancellationToken cancellationToken);
        Task<UserPrivilegeResponse> GetMyPrivileges(Guid userId, CancellationToken cancellationToken);
        Task UpdateUser(UpdateUser updateUser, CancellationToken cancellationToken);
        Task<GetUserDetailResponse> GetUserDetail(Guid id, CancellationToken cancellationToken);
    }
}
