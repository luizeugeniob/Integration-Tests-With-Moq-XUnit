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

        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
