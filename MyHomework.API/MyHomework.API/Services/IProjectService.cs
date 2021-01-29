using System.Collections.Generic;
using System.Threading.Tasks;
using MyHomework.API.Dtos;
using MyHomework.API.Entities;

namespace MyHomework.API.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsBySubjectIdAsync(int id);
        Task<bool> CreateAsync(Project project);
            
        Task<bool> EnrollInProjectAsync(int projectId,      
            int studentId,
            string githubLink);

        Task<bool> UpdateAsync(int projectId, ProjectForUpdateDto projectForUpdateDto);

        Task<Project> GetAsync(int id);
        Task<bool> DeleteAsync(int projectId);
    }   
}
        