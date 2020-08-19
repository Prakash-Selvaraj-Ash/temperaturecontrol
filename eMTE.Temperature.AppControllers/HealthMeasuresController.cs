using System;
using System.IO;
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
        public async Task<IActionResult> CreateOrUpdateHealthMeasure(CreateDayMeasure createDayMeasure, CancellationToken cancellationToken = default)
        {
            await _healthMeasureService.CreateMeasure(createDayMeasure, cancellationToken);
            return Ok();
        }


        [Authorize]
        [HttpGet("getDayMeasure")]
        public async Task<IActionResult> GetDayMeasure(DateTime dateTime, CancellationToken cancellationToken = default)
        {
            var measures = await _healthMeasureService.GetDayMeasure(dateTime, cancellationToken);
            return new OkObjectResult(measures);
        }

        [Authorize]
        [HttpPut("getExportData")]
        public async Task<IActionResult> GetExportData(GetExportRequest getExportRequest, CancellationToken cancellationToken = default)
        {
            var data = await _healthMeasureService.GetExportRows(getExportRequest.TeamId, getExportRequest.StartDate, getExportRequest.EndDate, cancellationToken);
            return new OkObjectResult(data);
        }

        [Authorize]
        [HttpPut("generateReport")]
        public async Task<IActionResult> GenerateExcel(GetExportRequest getExportRequest, CancellationToken cancellationToken = default)
        {
            var data = await _healthMeasureService.Export(getExportRequest.TeamId, getExportRequest.StartDate, getExportRequest.EndDate, cancellationToken);
            var fileName = string.Format("{0}-{1}-{2}-Health_Tracker.xlsx", getExportRequest.TeamId, getExportRequest.StartDate, getExportRequest.EndDate);
            return File(
                        data,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
        }

        [Authorize]
        [HttpPut("getDashBoardData")]
        public async Task<IActionResult> GetDashBoardData([FromBody]GetDashBoardRequest getDashBoardRequest, CancellationToken cancellationToken = default)
        {
            var data = await _healthMeasureService.GetDashBoardData(getDashBoardRequest.TeamId, getDashBoardRequest.DateTime, cancellationToken);
            return new OkObjectResult(data);
        }
    }
}
