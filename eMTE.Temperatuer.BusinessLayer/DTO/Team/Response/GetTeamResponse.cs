using System;
namespace eMTE.Temperature.BusinessLayer.DTO.Team.Response
{
    public class GetTeamResponse : WithAuditBase
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string TeamDescription { get; set; }
        public int MembersCount { get; set; }
        public Guid ManagerId { get; set; }

    }
}
