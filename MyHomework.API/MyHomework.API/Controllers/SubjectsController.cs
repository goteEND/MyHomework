using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyHomework.API.Dtos;
using MyHomework.API.Services;

namespace MyHomework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public SubjectsController(
            ISubjectService subjectService,
            IProjectService projectService,
            IMapper mapper
            )
        {
            _subjectService = subjectService;
            _projectService = projectService;
            _mapper = mapper;
        }


        // public
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            var subjectForReturn = _mapper.Map<SubjectForReturnDto>(subject);   
            
            return Ok(subjectForReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();

            var subjectsForReturn = _mapper.Map<IEnumerable<SubjectForReturnDto>>(subjects);

            return Ok(subjectsForReturn);
        }


        [HttpGet("{id}/projects")]
        public async Task<IActionResult> GetProjectsBySubjectId(int id)
        {
            var projects = await _projectService.GetAllProjectsBySubjectIdAsync(id);

            var projectsForReturn = _mapper.Map<IEnumerable<ProjectForReturnDto>>(projects);

            return Ok(projectsForReturn);
        }



    }       
}   
        