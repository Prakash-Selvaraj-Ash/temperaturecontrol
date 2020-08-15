using System;
namespace eMTE.Common.Domain
{
    public interface IWithAuditFields
    {
         Guid CreatedById { get; set; }
         DateTime CreatedTime { get; set; }
         Guid? ModifiedById { get; set; }
         DateTime? ModifiedTime { get; set; }
    }
}
