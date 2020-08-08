using System;
using System.Collections;
using System.Collections.Generic;

namespace eMTE.Common.Export.ExcelExport.DTO
{
    public class Row
    {
        public IEnumerable<Column> Cells { get; set; }
    }
}
