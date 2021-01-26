using System;

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
        public Subject Subject { get; set; }
    }
}       
