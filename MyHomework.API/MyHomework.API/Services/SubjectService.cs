using MyHomework.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyHomework.API.Persistance.Interfaces;

namespace MyHomework.API.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task<Subject> GetSubjectByIdAsync(int id)
        {
            return await _subjectRepository.GetAsync(id);
        }
    }
}
