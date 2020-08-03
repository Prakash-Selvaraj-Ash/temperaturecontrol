using System;
using AutoMapper;
using eMTE.Temperature.BusinessLayer.DTO.Team.Request;
using eMTE.Temperature.BusinessLayer.DTO.Team.Response;
using eMTE.Temperature.Domain;

namespace eMTE.Temperature.BusinessLayer.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<CreateTeam, Team>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.TeamManagerId, opts => opts.Ignore())
                .ForMember(dest => dest.OrganizationId, opts => opts.Ignore());

            CreateMap<Team, GetTeamResponse>()
                .ForMember(dest => dest.TeamId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.ManagerId, opts => opts.MapFrom(src => src.TeamManagerId));
                
        }
    }
}
