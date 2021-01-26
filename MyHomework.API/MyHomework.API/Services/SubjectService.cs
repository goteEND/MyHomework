using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyHomework.API.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyHomework.API.Persistance;

namespace MyHomework.API.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly DataContext _dataContext;
        public SubjectService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Subject> GetSubjectByIdAsync(int id)
        {
            return await _dataContext.Subjects
                .FirstOrDefaultAsync(sbj => sbj.Id == id);
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _dataContext.Subjects
                .ToListAsync();
        }
    }   
}
