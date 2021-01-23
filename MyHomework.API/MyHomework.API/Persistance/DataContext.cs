using Microsoft.EntityFrameworkCore;
using MyHomework.API.Entities;

namespace MyHomework.API.Persistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            :base(options)
        {
        }  
                    
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
