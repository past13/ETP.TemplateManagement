using AutoMapper;
using ETP.TemplatesManagement.Data.Entities;
using ETP.TemplatesManagement.SDK.Dto;

namespace ETP.TemplatesManagement.ServiceHost.Mappers;

public class AttributeMapper : Profile
{
    public AttributeMapper()
    {
        CreateMap<TemplateAttributeEntity, TemplateAttributeDto>()
            .ReverseMap();
    }
}