using System;

namespace ToDo.Core.Models
{
    /// <summary>
    /// Representa uma tarefa a ser realizada.
    /// </summary>
    public class ToDoTask
    {
        public ToDoTask()
        {

        }

        public ToDoTask(int id, string title, Category category, DateTime deadline, DateTime? completionDate, ToDoTaskStatus status)
        {
            Id = id;
            Title = title;
            Category = category;
            Deadline = deadline;
            CompletionDate = completionDate;
            Status = status;
        }

        /// <summary>
        /// Identificador da tarefa.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Título da tarefa.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Categoria da tarefa.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Prazo da tarefa.
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Indica quando a tarefa foi concluída.
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// Estado atual da tarefa.
        /// </summary>
        public ToDoTaskStatus Status { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Title}, {Category.Name}, {Deadline:dd/MM/yyyy}";
        }
    }
}
