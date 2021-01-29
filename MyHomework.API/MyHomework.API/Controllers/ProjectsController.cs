using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyHomework.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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


        // studenti

        [HttpPatch("{projectId}/enrolledStudent/{studentId}")]
        public async Task<IActionResult> EnrollInProject(int projectId,
            int studentId,
            ProjectForEnrollmentDto projectForEnrollmentDto)
        {
            var successResult = await _projectService
                .EnrollInProject(projectId, studentId, projectForEnrollmentDto.GithubLink);
                
            if (successResult)
             return Ok();
                
            return Ok("blah");
        }


        // profesori

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody]ProjectForCreateDto projectForCreateDto)
        {
            var project =
                _mapper.Map<Project>(projectForCreateDto);


            var isSuccess = await _projectService.Create(project);

            if (isSuccess)
                return Ok();


            return StatusCode(StatusCodes.Status500InternalServerError,
                "Something went wrong while saving the project in the database");
        }

        [HttpPut("{projectId}")]

        public async Task<IActionResult> Update(int projectId, [FromBody]ProjectForUpdateDto projectForUpdateDto)
        {
            var successResult =  await _projectService.Update(projectId, projectForUpdateDto);

            if (successResult)
                return Ok();

            return Ok("blah");
        }   
    }   
}   
    