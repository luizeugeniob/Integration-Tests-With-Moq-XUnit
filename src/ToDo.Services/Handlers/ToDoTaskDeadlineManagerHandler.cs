using System.Linq;
using ToDo.Core.Commands;
using ToDo.Core.Models;
using ToDo.Infrastructure;

namespace ToDo.Services.Handlers
{
    public class ToDoTaskDeadlineManagerHandler
    {
        IToDoTaskRepository _repository;

        public ToDoTaskDeadlineManagerHandler()
        {
            _repository = new ToDoTaskRepository();
        }

        public void Execute(ToDoTaskDeadlineManager command)
        {
            var now = command.DateTimeNow;

            //pegar todas as tarefas não concluídas que passaram do prazo
            var tasks = _repository
                .GetTasks(t => t.Deadline <= now && t.Status != ToDoTaskStatus.Completed)
                .ToList();

            //atualizá-las com status Atrasada
            tasks.ForEach(t => t.Status = ToDoTaskStatus.Delayed);

            //salvar tarefas
            _repository.UpdateTasks(tasks.ToArray());
        }
    }
}
