using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    