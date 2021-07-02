using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using RookieOnlineAssetManagement.Services.Report.Query;
using RookieOnlineAssetManagement.ViewModels.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : BaseController
    {
        [Authorize(Policy = "IsDisable")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportReadModel>>> GetReport()
        {
            return HandleResult(await Mediator.Send(new GetReportQuery { Location = UserLocation }));
        }


        [Authorize(Policy = "IsDisable")]
        [HttpGet("export")]
        public async Task<IActionResult> ExportReport()
        {
            var reportApiCall = await Mediator.Send(new GetReportQuery { Location = UserLocation });
            string excelName = $"Report-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (reportApiCall.IsSuccess)
            {
                var reportFile = await Mediator.Send(new GetReportFileQuery { reportList = reportApiCall.Value });
                return new FileContentResult(reportFile.Value.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = excelName
                };
            }
            else
            {
                return NoContent();
            }
        }
    }
}
