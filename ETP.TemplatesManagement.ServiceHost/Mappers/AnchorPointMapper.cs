using AutoMapper;
using ETP.TemplatesManagement.Data.Entities;
using ETP.TemplatesManagement.SDK.Dto;

namespace ETP.TemplatesManagement.ServiceHost.Mappers;

public class AnchorPointMapper : Profile
{
    public AnchorPointMapper()
    {
        CreateMap<AnchorPointEntity, AnchorPointDto>()
            .ReverseMap();
    }
}