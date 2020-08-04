using System;
using eMTE.Common.Tools.Contract;

namespace eMTE.Common.Tools.Implementation
{
    public class DateTimeToolService : IDateTimeToolService
    {
        public DateTimeOffset DateTimeOffset => DateTimeOffset.UtcNow;

        public DateTime DateTime => DateTime.UtcNow;
    }
}
