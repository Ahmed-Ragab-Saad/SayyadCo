using AutoMapper;
using SayyadCo.Application.Common.DTOs;
using SayyadCo.Application.Features.Tests.Commands.CreateTest;
using SayyadCo.Application.Features.Tests.Commands.UpdateTest;
using SayyadCo.Application.Features.Tests.Queries.GetAllTests;
using SayyadCo.Application.Features.Tests.Queries.GetTestById;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<CreateTestCommand, Test>()
                .ForMember(dest => dest.Questions, opt => opt.Ignore());
            CreateMap<Test, CreateTestResponseDto>();

            CreateMap<UpdateTestCommand, Test>()
                .ForMember(dest => dest.SectionId, opt => opt.Ignore())
                .ForMember(dest => dest.GameId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Questions, opt => opt.Ignore());

            CreateMap<UpdateQuestionDto, Question>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Test, GetTestByIdResponseDto>();
            CreateMap<Question, QuestionDto>();

            CreateMap<Test, GetAllTestsResponseDto>()
                .ForMember(dest => dest.QuestionsCount, opt => opt.MapFrom(src => src.Questions.Count));
        }
    }
}
