using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Core.Models;

namespace ToDo.Infrastructure
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        DbToDoTasksContext _context;

        public ToDoTaskRepository(DbToDoTasksContext context)
        {
            _context = context;
        }

        public void InsertTasks(params ToDoTask[] tasks)
        {
            _context.ToDoTasks.AddRange(tasks);
            _context.SaveChanges();

        }

        public void UpdateTasks(params ToDoTask[] tasks)
        {
            _context.ToDoTasks.UpdateRange(tasks);
            _context.SaveChanges();
        }

        public void DeleteTasks(params ToDoTask[] tasks)
        {
            _context.ToDoTasks.RemoveRange(tasks);
            _context.SaveChanges();
        }

        Category IToDoTaskRepository.GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<ToDoTask> GetTasks(Func<ToDoTask, bool> filter)
        {
            return _context.ToDoTasks.Where(filter);
        }
    }
}
