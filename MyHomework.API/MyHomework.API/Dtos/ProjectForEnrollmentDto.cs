using System.ComponentModel.DataAnnotations;

namespace MyHomework.API.Dtos
{
    public class ProjectForEnrollmentDto
    {
        [Required]
        public string GithubLink { get; set; }
    }
}
