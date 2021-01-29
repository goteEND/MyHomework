using System;
using System.ComponentModel.DataAnnotations;

namespace MyHomework.API.Dtos
{
    public class ProjectForUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int? EnrolledStudentId { get; set; }
        public string GithubLink { get; set; }
        public DateTime? DueDate { get; set; }
        [Required]
        public int SubjectId { get; set; }
    }
}
