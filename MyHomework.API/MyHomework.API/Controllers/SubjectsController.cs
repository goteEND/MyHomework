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
        public SubjectsController(ISubjectService subjectService) 
        {
            _subjectService = subjectService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _subjectService.GetSubjectByIdAsync(id));
        }
    }
}
