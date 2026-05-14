using AutoMapper;
using SayyadCo.Application.Features.Exams.Commands.CreateExam;
using SayyadCo.Application.Features.Exams.Queries.GetExamById;
using SayyadCo.Application.Features.Exams.Queries.GetPendingExams;
using SayyadCo.Application.Features.Exams.Queries.GetPendingExamUpdates;
using SayyadCo.Application.Features.Tests.Commands.AddQuestions;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class ExamMappingProfile : Profile
    {
        public ExamMappingProfile()
        {
            CreateMap<CreateExamCommand, Exam>()
                .ForMember(x => x.Questions, opt => opt.Ignore());
            CreateMap<Question, ExamQuestionsResponse>();
            CreateMap<Exam, CreateExamResponseDto>();
            CreateMap<Question, AddQuestionResponseDto>();
            CreateMap<Exam, GetExamByIdResponseDto>();
            CreateMap<Exam, GetPendingExamsResponseDto>()
                .ForMember(dest => dest.QuestionsCount,
                    opt => opt.MapFrom(src => src.Questions.Count));
            CreateMap<ExamUpdateRequest, GetPendingExamUpdatesResponseDto>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ExamTitleEn, opt => opt.MapFrom(src => src.Exam.TitleEn));
        }

    }
}
