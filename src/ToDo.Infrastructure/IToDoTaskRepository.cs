using System;
using System.Collections.Generic;
using ToDo.Core.Models;

namespace ToDo.Infrastructure
{
    public interface IToDoTaskRepository
    {
        void InsertTasks(params ToDoTask[] tasks);
        void UpdateTasks(params ToDoTask[] tasks);
        void DeleteTasks(params ToDoTask[] tasks);

        Category GetCategoryById(int id);
        IEnumerable<ToDoTask> GetTasks(Func<ToDoTask, bool> filter);
    }
}
