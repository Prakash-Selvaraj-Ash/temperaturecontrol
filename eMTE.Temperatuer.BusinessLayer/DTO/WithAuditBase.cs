using System;
namespace eMTE.Temperature.BusinessLayer.DTO
{
    public class WithAuditBase
    {
        public Guid CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }
}
