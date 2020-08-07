using System;
using System.Linq;
using System.Security.Claims;
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
        private readonly IUserService _userService;
        public UsersController(
            IUserService userService,
            IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<OkObjectResult> Login(LoginRequest loginRequest, CancellationToken cancellationToken = default)
        {
            var token = await _authenticationService.Login(loginRequest.Email, loginRequest.Password, cancellationToken);
            return new OkObjectResult(token);
        }

        [AllowAnonymous]
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(CreateUser createUser, CancellationToken cancellationToken = default)
        {
            await _userService.CreateUser(createUser, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpGet("previleges")]
        public async Task<IActionResult> GetPrevilige(CancellationToken cancellationToken = default)
        {
            var claims = HttpContext.User.Claims;
            var userId = Guid.Parse(claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            var privileges = await _userService.GetMyPrivileges(userId, cancellationToken);
            return new OkObjectResult(privileges);
        }

        [Authorize]
        [HttpGet("detail/{userId}")]
        public async Task<IActionResult> GetUserDetail(Guid userId, CancellationToken cancellationToken = default)
        {
            var userDetail = await _userService.GetUserDetail(userId, cancellationToken);
            return new OkObjectResult(userDetail);

        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateUser updateUser, CancellationToken cancellationToken = default)
        {
            await _userService.UpdateUser(updateUser, cancellationToken);
            return Ok();

        }
    }
}
