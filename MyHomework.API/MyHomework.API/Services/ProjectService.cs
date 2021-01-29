using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyHomework.API.Dtos;
using MyHomework.API.Entities;
using MyHomework.API.Persistance;

namespace MyHomework.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ProjectService(
            DataContext dataContext,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Project>> GetAllProjectsBySubjectIdAsync(int id)
        {
            var subject = await _dataContext.Subjects
                .Include(sbj => sbj.Projects)
                .FirstOrDefaultAsync(sbj => sbj.Id == id);

            return subject.Projects;
        }

        public async Task<bool> Create(Project project)
        {
            _dataContext.Projects.Add(project);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> EnrollInProject(int projectId, int studentId, string githubLink)
        {
            await UnEnrollFromAllProjectsInSubject(studentId, projectId);

            var project = await Get(projectId);

            project.EnrolledStudentId = studentId;
            project.GithubLink = githubLink;

            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(int projectId, ProjectForUpdateDto projectForUpdateDto)
        {
            var project = await Get(projectId);

            _mapper.Map(projectForUpdateDto, project);

            return await _dataContext.SaveChangesAsync() > 0;
        }


        public async Task<Project> Get(int id)
            => await _dataContext.Projects
                .FirstOrDefaultAsync(prj => prj.Id == id);

        public async Task<bool> Delete(int projectId)
        {
            _dataContext.Projects.Remove(await Get(projectId));

            return await _dataContext.SaveChangesAsync() > 0;
        }

        private async Task<bool> UnEnrollFromAllProjectsInSubject(int studentId, int subjectId)
        {
            var projects = await _dataContext.Projects
                .Where(prj => prj.EnrolledStudentId == studentId)
                .ToListAsync();

            if (!projects.Any())
                return true;

            foreach (var project in projects)
            {
                project.EnrolledStudentId = null;
                project.GithubLink = null;
            }

            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
