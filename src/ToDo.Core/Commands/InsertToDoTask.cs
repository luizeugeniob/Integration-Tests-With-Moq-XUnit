using System;
using ToDo.Core.Models;

namespace ToDo.Core.Commands
{
    /// <summary>
    /// Informações necessárias para cadastrar uma tarefa.
    /// </summary>
    public class InsertToDoTask
    {
        public InsertToDoTask(string title, Category category, DateTime deadline)
        {
            Title = title;
            Category = category;
            Deadline = deadline;
        }

        public string Title { get; }
        public Category Category { get; }
        public DateTime Deadline { get; }
    }
}
