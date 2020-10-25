namespace ToDo.Core.Models
{
    /// <summary>
    /// Indica o estado em que a <see cref="ToDoTask"/> se encontra no presente.
    /// </summary>
    public enum ToDoTaskStatus
    {
        Created,
        Pending,
        Delayed,
        Completed
    }
}
