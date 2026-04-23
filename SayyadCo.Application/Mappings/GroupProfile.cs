using AutoMapper;
using SayyadCo.Application.Features.Groups.Commands.CreateGroup;
using SayyadCo.Application.Features.Groups.Commands.UpdateGroup;
using SayyadCo.Application.Features.Groups.Queries;
using SayyadCo.Application.Features.Groups.Queries.GetGroupById;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Application.Mappings
{
    public class GroupMappingProfile : Profile
    {
        public GroupMappingProfile()
        {
            CreateMap<Group, CreateGroupResponseDto>();
            CreateMap<Group, GetAllGroupsResponseDto>();

            CreateMap<UpdateGroupCommand, Group>();
            CreateMap<Group, UpdateGroupResponseDto>();

            CreateMap<Group, UpdateGroupResponseDto>();
            CreateMap<Group, GetGroupByIdResponseDto>();
            CreateMap<Exam, GroupExamDto>();
        }
    }
}
