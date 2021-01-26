using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyHomework.API.Entities;
using MyHomework.API.Persistance;

namespace MyHomework.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DataContext _dataContext;

        public ProjectService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<Project>> GetAllProjectsBySubjectIdAsync(int id)
        {
            var subject = await _dataContext.Subjects
                .Include(sbj => sbj.Projects)
                .FirstOrDefaultAsync(sbj => sbj.Id == id);

            return subject.Projects;
        }
    }
}
