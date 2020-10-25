using ToDo.Core.Commands;
using ToDo.Core.Models;
using ToDo.Infrastructure;

namespace ToDo.Services.Handlers
{
    public class GetCategoryByIdHandler
    {
        IToDoTaskRepository _repository;

        public GetCategoryByIdHandler()
        {
            _repository = new ToDoTaskRepository();
        }
        public Category Execute(GetCategoryById command)
        {
            return _repository.GetCategoryById(command.Id);
        }
    }
}
