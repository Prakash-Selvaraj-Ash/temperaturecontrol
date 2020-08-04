using System;
namespace eMTE.Common.Domain
{
    public interface IWithAuditFields
    {
        public Guid CreatedById { get; set; }
        public DateTime CreatedTime { get; set; }
        public Guid? ModifiedById { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }
}
