using AutoMapper;
using SayyadCo.Application.Features.AcademicYears.Commands.AddAcademicYear;
using SayyadCo.Application.Features.AcademicYears.Commands.UpdateAcademicYear;
using SayyadCo.Application.Features.AcademicYears.Queries.GetAllAcademicYears;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class AcademicYearProfile : Profile
    {
        public AcademicYearProfile()
        {
            CreateMap<AddAcademicYearCommand, AcademicYear>();
            CreateMap<AcademicYear, AddAcademicYearResponseDto>();

            CreateMap<UpdateAcademicYearCommand, AcademicYear>()
                .ForMember(ay => ay.Id, opt => opt.Ignore());

            CreateMap<AcademicYear, UpdateAcademicYearResponseDto>();

            CreateMap<AcademicYear, GetAllAcademicYearsResponseDto>();
        }
    }
}
