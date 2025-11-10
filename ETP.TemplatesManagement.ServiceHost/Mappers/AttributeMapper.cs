using AutoMapper;
using ETP.TemplatesManagement.Data.Entities;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Domain.Request;

namespace ETP.TemplatesManagement.ServiceHost.Mappers;

public class AttributeMapper : Profile
{
    public AttributeMapper()
    {
        CreateMap<TemplateAttributeEntity, TemplateAttributeDto>()
            .ReverseMap();
        
        CreateMap<TemplateAttributeRequest, TemplateAttributeEntity>()
            .ReverseMap();
    }
}