using System;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Request;
using eMTE.Temperature.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eMTE.Temperature.AppControllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class HealthMeasuresController : Controller
    {
        private readonly IHealthMeasureService _healthMeasureService;

        public HealthMeasuresController(
            IHealthMeasureService healthMeasureService)
        {
            _healthMeasureService = healthMeasureService;
        }

        [Authorize]
        [HttpPost("createOrUpdate")]
        public async Task<IActionResult> CreateOrUpdateHealthMeasure(CreateDayMeasure createDayMeasure, CancellationToken cancellationToken)
        {
            await _healthMeasureService.CreateMeasure(createDayMeasure, cancellationToken);
            return Ok();
        }


        [Authorize]
        [HttpGet("getDayMeasure")]
        public async Task<IActionResult> GetDayMeasure(DateTime dateTime, CancellationToken cancellationToken)
        {
            var measures = await _healthMeasureService.GetDayMeasure(dateTime, cancellationToken);
            return new OkObjectResult(measures);
        }
    }
}
