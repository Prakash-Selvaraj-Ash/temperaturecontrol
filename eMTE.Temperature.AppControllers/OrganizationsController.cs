using System;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Temperature.BusinessLayer.DTO.Organization.Request;
using eMTE.Temperature.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eMTE.Temperature.AppControllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class OrganizationsController : Controller
    {
        private readonly IOrganizationService _organizationService;
        public OrganizationsController(
            IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterOrganization(CreateOrganization createOrganization, CancellationToken cancellationToken = default)
        {
            await _organizationService.RegisterOrganization(createOrganization, cancellationToken);
            return Ok();
        }
    }
}
