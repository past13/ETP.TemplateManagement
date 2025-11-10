using AutoMapper;
using ETP.TemplatesManagement.Data.Entities;
using ETP.TemplatesManagement.SDK.Dto;

namespace ETP.TemplatesManagement.ServiceHost;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TemplateEntity, TemplateDto>();
    }
}