using System;

namespace eMTE.Common.Tools.Contract
{
    public interface IDateTimeToolService
    {
        DateTimeOffset DateTimeOffset { get; }
        DateTime DateTime { get; }
    }
}
