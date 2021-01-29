using System.Collections.Generic;
using System.Threading.Tasks;
using MyHomework.API.Dtos;
using MyHomework.API.Entities;

namespace MyHomework.API.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsBySubjectIdAsync(int id);
        Task<bool> Create(Project project);
            
        Task<bool> EnrollInProject(int projectId,
            int studentId,
            string githubLink);

        Task<bool> Update(int projectId, ProjectForUpdateDto projectForUpdateDto);

        Task<Project> Get(int id);
        Task<bool> Delete(int projectId);
    }
}
        