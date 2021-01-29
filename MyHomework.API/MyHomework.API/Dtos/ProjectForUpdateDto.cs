using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
