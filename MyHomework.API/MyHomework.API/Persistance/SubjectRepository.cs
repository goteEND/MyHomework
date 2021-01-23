﻿using MyHomework.API.Entities;
using MyHomework.API.Persistance.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace MyHomework.API.Persistance
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        private readonly DataContext _context;
        public SubjectRepository(DataContext context)
            : base(context)
        {
            _context = context;
        }

        public override async Task<Subject> GetAsync(int id)
        {
            return await Context.Subjects
                .Include(subj => subj.Projects)
                .FirstOrDefaultAsync(subj => subj.Id == id);
        }
    }
}
    