using System;
using System.Collections.Generic;
using eMTE.Common.Export.ExcelExport.DTO;

namespace eMTE.Common.Export.ExcelExport
{
    public interface IExcelExportService
    {
        byte[] Export(IEnumerable<Row> rows);
    }
}
