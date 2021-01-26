using System.Collections.Generic;
using System.Threading.Tasks;
using MyHomework.API.Entities;

namespace MyHomework.API.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsBySubjectIdAsync(int id);
    }
}
