using Microsoft.EntityFrameworkCore;
using ToDo.Core.Models;

namespace ToDo.Infrastructure
{
    public class DbToDoTasksContext : DbContext
    {
        public DbToDoTasksContext(DbContextOptions options) : base(options)
        {
        }

        public DbToDoTasksContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ToDoDB;Trusted_Connction=true;");
        }

        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
