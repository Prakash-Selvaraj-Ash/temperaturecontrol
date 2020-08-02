using System;
namespace eMTE.Temperature.BusinessLayer.DTO.Team.Request
{
    public class CreateTeam
    {
        public string Name { get; set; }
        public Guid ManagerId { get; set; }
    }
}
