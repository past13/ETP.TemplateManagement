using AutoMapper;
using ETP.TemplatesManagement.Data.Entities;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Domain.Request;

namespace ETP.TemplatesManagement.ServiceHost.Mappers;

public class TemplateMapper : Profile
{
    public TemplateMapper()
    {
        CreateMap<TemplateEntity, TemplateDto>()
            .ReverseMap();
        
        CreateMap<TemplateRequest, TemplateEntity>()
            .ReverseMap();
    }
}