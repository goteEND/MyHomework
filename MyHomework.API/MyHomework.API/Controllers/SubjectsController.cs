using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyHomework.API.Services;

namespace MyHomework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly IProjectService _projectService;

        public SubjectsController(
            ISubjectService subjectService,
            IProjectService projectService
            )
        {
            _subjectService = subjectService;
            _projectService = projectService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _subjectService.GetSubjectByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _subjectService.GetAllSubjectsAsync());
        }


        [HttpGet("{id}/projects")]
        public async Task<IActionResult> GetProjectsBySubjectId(int id)
        {
            return Ok(await _projectService.GetAllProjectsBySubjectIdAsync(id));
        }
    }
}   
    