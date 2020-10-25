using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Core.Models;
using ToDo.Infrastructure;

namespace ToDo.Tests
{
    public class ToDoTaskRepositoryFake : IToDoTaskRepository
    {
        private readonly List<ToDoTask> _toDoTask = new List<ToDoTask>();

        public void InsertTasks(params ToDoTask[] tasks)
        {
            _toDoTask.ToList().ForEach(t => _toDoTask.Add(t));
        }

        public void UpdateTasks(params ToDoTask[] tasks)
        {
            throw new NotImplementedException();
        }

        public void DeleteTasks(params ToDoTask[] tasks)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ToDoTask> GetTasks(Func<ToDoTask, bool> filter)
        {
            return _toDoTask.Where(filter);
        }
    }
}