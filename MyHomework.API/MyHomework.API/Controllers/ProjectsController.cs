using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyHomework.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyHomework.API.Dtos;
using MyHomework.API.Entities;

namespace MyHomework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public ProjectsController(
            IProjectService projectService,
            ISubjectService subjectService,
            IMapper mapper)
        {
            _projectService = projectService;
            _subjectService = subjectService;
            _mapper = mapper;
        }


        // student sau profesori
        [HttpGet("{id}", Name = "GetProject")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200, Type = typeof(ProjectForReturnDto))]
        public async Task<IActionResult> GetAsync(int id)
        {
            var project = await _projectService.GetAsync(id);

            if (project == null)
                return NoContent();

            var projectForReturn = _mapper.Map<ProjectForReturnDto>(project);

            return Ok(projectForReturn);
        }
            
        // studenti
        [HttpPatch("{id}/enrolledStudent/{studentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> EnrollAsync(int id,
            int studentId,
            ProjectForEnrollmentDto projectForEnrollmentDto)
        {
            var project = await _projectService.GetAsync(id);
            if (project == null)
                return BadRequest($@"Project with id {id} does not exist");

            // todo check if user exists and is legit


            var successResult = await _projectService
                .EnrollInProjectAsync(id, studentId, projectForEnrollmentDto.GithubLink);
                
            if (successResult)
             return Ok();

            return StatusCode(StatusCodes.Status500InternalServerError,
                "Something went wrong while enrolling student in the database.");
        }


        // profesori
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201, Type = typeof(ProjectForReturnDto))]
        public async Task<IActionResult> CreateAsync([FromBody]ProjectForCreateDto projectForCreateDto)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(projectForCreateDto.SubjectId);
            if (subject == null)
                return BadRequest($@"No Subject with {projectForCreateDto.SubjectId} has been found");

            var project =
                _mapper.Map<Project>(projectForCreateDto);  

            var isSuccess = await _projectService.CreateAsync(project);

            if (!isSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Something went wrong while saving the project in the database");

            var projectForReturnDto = _mapper.Map<ProjectForReturnDto>(project);

            return CreatedAtRoute("GetProject", new {id = projectForReturnDto.Id}, projectForReturnDto);
        }   

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]ProjectForUpdateDto projectForUpdateDto)
        {
            var project = await _projectService.GetAsync(id);
            if (project == null)
                return BadRequest($@"Project with id {id} does not exist");

            var successResult =  await _projectService.UpdateAsync(id, projectForUpdateDto);

            if (successResult)
                return NoContent();

            return StatusCode(StatusCodes.Status500InternalServerError,
                "Something went wrong while updating project in the database.");
        }   

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var project = await _projectService.GetAsync(id);
            if (project == null)
                return BadRequest($@"Project with id {id} does not exist");

            var successResult = await _projectService.DeleteAsync(id);

            if (successResult)
                return NoContent();

            return StatusCode(StatusCodes.Status500InternalServerError,
                "Something went wrong while deleting project in the database.");
        }

    }
}   
    