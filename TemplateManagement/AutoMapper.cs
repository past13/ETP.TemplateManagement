using AutoMapper;
using TemplateManagement.Domain.Entities;
using TemplateManagement.Dto;

namespace TemplateManagement;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TemplateEntity, TemplateDto>();
    }
}