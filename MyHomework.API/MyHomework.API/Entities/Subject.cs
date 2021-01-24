using System.Collections.Generic;

namespace MyHomework.API.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; }  
    }
}
    