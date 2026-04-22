using AutoMapper;
using SayyadCo.Application.Features.Exams.Commands.CreateExam;
using SayyadCo.Application.Features.Tests.Commands.AddQuestions;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class ExamMappingProfile : Profile
    {
        public ExamMappingProfile()
        {
            CreateMap<CreateExamCommand, Exam>();
            CreateMap<Exam, CreateExamResponseDto>();
            CreateMap<Question, AddQuestionResponseDto>();
        }

    }
}
