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
        private readonly IMapper _mapper;

        public ProjectsController(
            IProjectService projectService,
            IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }



        // student sau profesori
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var project = await _projectService.GetAsync(id);

            var projectForReturn = _mapper.Map<ProjectForReturnDto>(project);

            return Ok(projectForReturn);
        }
            
        // studenti

        [HttpPatch("{id}/enrolledStudent/{studentId}")]
        public async Task<IActionResult> EnrollAsync(int id,
            int studentId,
            ProjectForEnrollmentDto projectForEnrollmentDto)
        {
            var successResult = await _projectService
                .EnrollInProjectAsync(id, studentId, projectForEnrollmentDto.GithubLink);
                
            if (successResult)
             return Ok();
                
            return Ok("blah");
        }


        // profesori

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]ProjectForCreateDto projectForCreateDto)
        {
            var project =
                _mapper.Map<Project>(projectForCreateDto);  


            var isSuccess = await _projectService.CreateAsync(project);

            if (isSuccess)
                return Ok();


            return StatusCode(StatusCodes.Status500InternalServerError,
                "Something went wrong while saving the project in the database");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]ProjectForUpdateDto projectForUpdateDto)
        {
            var successResult =  await _projectService.UpdateAsync(id, projectForUpdateDto);

            if (successResult)
                return Ok();
                
            return Ok("blah");  
        }   

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var successResult = await _projectService.DeleteAsync(id);

            if (successResult)
                return Ok();

            return Ok("blah");
        }

    }
}   
    