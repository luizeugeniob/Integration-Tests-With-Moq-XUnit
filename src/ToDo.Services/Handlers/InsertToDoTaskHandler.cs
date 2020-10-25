using Microsoft.Extensions.Logging;
using ToDo.Core.Commands;
using ToDo.Core.Models;
using ToDo.Infrastructure;

namespace ToDo.Services.Handlers
{
    public class InsertToDoTaskHandler
    {
        IToDoTaskRepository _repository;
        ILogger<InsertToDoTaskHandler> _logger;

        public InsertToDoTaskHandler()
        {
            _repository = new ToDoTaskRepository();
            _logger = new LoggerFactory().CreateLogger<InsertToDoTaskHandler>();
        }

        public void Execute(InsertToDoTask command)
        {
            var task = new ToDoTask
            (
                id: 0,
                title: command.Title,
                deadline: command.Deadline,
                category: command.Category,
                completionDate: null,
                status: ToDoTaskStatus.Created
            );

            _logger.LogDebug("Persistindo a tarefa...");
            _repository.InsertTasks(task);
        }
    }
}
