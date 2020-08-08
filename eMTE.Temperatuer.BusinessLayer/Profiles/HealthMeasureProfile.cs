using System;
using System.Linq;
using AutoMapper;
using eMTE.Common.Export.ExcelExport.DTO;
using eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Request;
using eMTE.Temperature.BusinessLayer.DTO.HealthMeasure.Response;
using eMTE.Temperature.BusinessLayer.Extensions;
using eMTE.Temperature.Domain;

namespace eMTE.Temperature.BusinessLayer.Profiles
{
    public class HealthMeasureProfile : Profile
    {
        public HealthMeasureProfile()
        {
            CreateMap<CreateDayMeasure, DayMeasure>()
                .ForMember(dest => dest.UserId, opts => opts.Ignore())
                .ForMember(dest => dest.OrganizationId, opts => opts.Ignore())
                .ForMember(dest => dest.Id, opts => opts.Ignore());

            CreateMap<CreateHealthMeasure, HealthMeasure>()
                .ForMember(dest => dest.DayMeasureId, opts => opts.Ignore());

            CreateMap<HealthMeasure, GetHealthMeasure>();

            CreateMap<Column, ExportColumn>()
                .ForMember(dest => dest.Index, opts => opts.MapFrom(src => GetIndex(src)));

            CreateMap<Row, ExportRow>()
                .ForMember(dest => dest.Cells, opts => opts.MapFrom(src => src.Cells.Select(cell => new ExportColumn(cell.Index, cell.EndIndex, cell.Value, cell.Merge))));
            
        }

        private int GetIndex(Column src)
        {
            return src.Index;
        }
    }
}
