using System;
namespace eMTE.Temperature.BusinessLayer.DTO.Team.Response
{
    public class GetTeamResponse
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public Guid ManagerId { get; set; }
    }
}
