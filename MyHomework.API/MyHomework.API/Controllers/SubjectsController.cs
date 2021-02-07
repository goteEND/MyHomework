using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyHomework.API.Dtos;
using MyHomework.API.Services;

namespace MyHomework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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


        [HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200, Type = typeof(SubjectForReturnDto))]
        public async Task<IActionResult> GetAsync(int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);

            if (subject == null)
                return NoContent();

            var subjectForReturn = _mapper.Map<SubjectForReturnDto>(subject);   
            
            return Ok(subjectForReturn);
        }

        [HttpGet]
        [ProducesResponseType(204)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SubjectForReturnDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();

            if (!subjects.Any())
                return NoContent();

            var subjectsForReturn = _mapper.Map<IEnumerable<SubjectForReturnDto>>(subjects);

            return Ok(subjectsForReturn);
        }


        [HttpGet("{id}/projects")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProjectForReturnDto>))]
        public async Task<IActionResult> GetProjectsBySubjectId(int id)
        {
            var projects = await _projectService.GetAllProjectsBySubjectIdAsync(id);

            if (projects == null || !projects.Any())
                return NoContent();

            var projectsForReturn = _mapper.Map<IEnumerable<ProjectForReturnDto>>(projects);

            return Ok(projectsForReturn);
        }
    }       
}   
        