using System.Threading.Tasks;
using MyHomework.API.Entities;
using System.Collections.Generic;

namespace MyHomework.API.Services
{
    public interface ISubjectService
    {
        Task<Subject> GetSubjectByIdAsync(int id);
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();
    }
}   
