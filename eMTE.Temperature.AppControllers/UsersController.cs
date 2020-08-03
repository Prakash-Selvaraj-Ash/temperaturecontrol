using System;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Temperature.BusinessLayer.DTO.User.Request;
using eMTE.Temperature.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eMTE.Temperature.AppControllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public UsersController(
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<OkObjectResult> Login(LoginRequest loginRequest, CancellationToken cancellationToken = default)
        {
            var token = await _authenticationService.Login(loginRequest.Email, loginRequest.Password, cancellationToken);
            return new OkObjectResult(token);
        }
    }
}
