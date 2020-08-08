using System;
namespace eMTE.Common.Export.ExcelExport.DTO
{
    public class Column
    {
        public Column(int index, int endIndex, string value, bool merge = false)
        {
            Index = index;
            EndIndex = endIndex;
            Value = value;
            Merge = merge;
        }
        public int Index { get; }
        public int EndIndex { get; }
        public bool Merge { get; }
        public string Value { get; }
    }
}
