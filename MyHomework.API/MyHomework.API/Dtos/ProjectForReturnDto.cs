using System;

namespace MyHomework.API.Dtos
{
    public class ProjectForReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? EnrolledStudentId { get; set; }
        public string GithubLink { get; set; }
        public DateTime? DueDate { get; set; }
        public int SubjectId { get; set; }
    }
}
