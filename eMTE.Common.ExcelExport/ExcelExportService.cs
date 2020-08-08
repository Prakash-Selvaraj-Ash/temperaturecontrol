using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using eMTE.Common.Export.ExcelExport;
using eMTE.Common.Export.ExcelExport.DTO;

namespace eMTE.Common.ExcelExport
{
    public class ExcelExportService : IExcelExportService
    {
        public byte[] Export(IEnumerable<Row> rows)
        {
            var rowWithIndex = rows.Select((row, index) => new { row, index });
            string sheetName = "Sheet";
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheetName);

                foreach (var row in rowWithIndex)
                {
                    int currentRow = row.index + 1;
                    foreach (var cell in row.row.Cells)
                    {
                        worksheet.Cell(currentRow, cell.Index).Value = cell.Value;

                        if (cell.Merge)
                        {
                            worksheet.Range(currentRow, cell.Index, currentRow, cell.EndIndex).Merge();
                        }
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }

            }
        }
    }
}
