using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyHomework.API.Entities;

namespace MyHomework.API.Services
{
    public interface ISubjectService
    {
        Task<Subject> GetSubjectByIdAsync(int id);
    }
}   
