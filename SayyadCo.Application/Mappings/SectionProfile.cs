using AutoMapper;
using SayyadCo.Application.Features.Sections.Commands.CreateSection;
using SayyadCo.Application.Features.Sections.Commands.UpdateSection;
using SayyadCo.Application.Features.Sections.Queries.GetAllSections;
using SayyadCo.Application.Features.Sections.Queries.GetSectionById;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class SectionProfile : Profile
    {
        public SectionProfile()
        {
            CreateMap<CreateSectionCommand, Section>();
            CreateMap<Section, CreateSectionResponseDto>();

            CreateMap<UpdateSectionCommand, Section>();
            CreateMap<Section, UpdateSectionResponseDto>();

            CreateMap<Section, GetAllSectionsResponseDto>();

            CreateMap<Section, GetSectionByIdResponseDto>();
        }
    }
}
