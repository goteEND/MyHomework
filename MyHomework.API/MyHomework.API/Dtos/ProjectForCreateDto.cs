using System;
using System.ComponentModel.DataAnnotations;

namespace MyHomework.API.Dtos
{
    public class ProjectForCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        [Required]
        public int SubjectId { get; set; }  
    }
}
    