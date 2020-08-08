using System;
using System.Collections.Generic;

namespace eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Response
{
    public class ExportRow
    {
        public IEnumerable<ExportColumn> Cells { get; set; }
    }
}
