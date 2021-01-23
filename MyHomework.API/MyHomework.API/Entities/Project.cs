using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHomework.API.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EnrolledStudent { get; set; }
        public string GithubLink { get; set; }
        public DateTime? DueDate { get; set; } 
    }
}
