using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using System;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // добавлять сопоставления в таком стиле нужно для всех объектов
            CreateMap<CpuMetricDto, CpuMetric>().ForMember(dbModel => dbModel.Time, o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<CpuMetric, CpuMetricDto>().ForMember(tm => tm.Time, time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));

            CreateMap<CpuMetricUpdateRequest, CpuMetric>();
            CreateMap<CpuMetricCreateRequest, CpuMetric>();

        }
    }
}
