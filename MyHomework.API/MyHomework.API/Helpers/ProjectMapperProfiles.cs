using AutoMapper;
using MyHomework.API.Dtos;
using MyHomework.API.Entities;

namespace MyHomework.API.Helpers
{
    public class ProjectMapperProfiles : Profile
    {
        public ProjectMapperProfiles()
        {
            CreateMap<ProjectForCreateDto, Project>();
            CreateMap<ProjectForEnrollmentDto, Project>();
            CreateMap<ProjectForUpdateDto, Project>();
            CreateMap<Project, ProjectForReturnDto>();


            CreateMap<Subject, SubjectForReturnDto>();
        }
    }
}
