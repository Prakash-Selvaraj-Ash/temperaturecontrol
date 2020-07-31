using System;
using eMTE.Common.Domain;

namespace eMTE.Temperature.Domain
{
    public class User : IDomain, IWithId
    {
        public Guid Id { get; set; }
    }
}
