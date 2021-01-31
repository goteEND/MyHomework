using AutoMapper;
using MyHomework.API.Dtos;
using MyHomework.API.Entities;

namespace MyHomework.API.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<ProjectForCreateDto, Project>();
            CreateMap<ProjectForEnrollmentDto, Project>();
            CreateMap<ProjectForUpdateDto, Project>();
            CreateMap<Project, ProjectForReturnDto>();
                

            CreateMap<Subject, SubjectForReturnDto>();

            CreateMap<UserForRegistrationDto, AppUser>();
            CreateMap<AppUser, EnrolledStudentDto>();
        }
    }
}
