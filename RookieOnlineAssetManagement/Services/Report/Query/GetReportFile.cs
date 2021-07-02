using MediatR;
using OfficeOpenXml;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.ViewModels.Report;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services.Report.Query
{
    public class GetReportFileQuery : IRequest<Result<MemoryStream>>
    {
        public IEnumerable<ReportReadModel> reportList;
    }
    public class GetReportFileQueryHandler : IRequestHandler<GetReportFileQuery, Result<MemoryStream>>
    {

        public GetReportFileQueryHandler () { }
        public async Task<Result<MemoryStream>> Handle (GetReportFileQuery query, CancellationToken cancellationToken)
        {
            if (query.reportList is null)
            {
                return Result<MemoryStream>.Failure("Report list is empty");
            }
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Asset");
                workSheet.Cells.LoadFromCollection(query.reportList, true);
                package.SaveAs(stream);
            }

            return Result<MemoryStream>.Success(stream);
        }
    }
}
