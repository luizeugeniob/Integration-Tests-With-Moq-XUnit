using ToDo.Core.Commands;
using ToDo.Core.Models;
using ToDo.Infrastructure;

namespace ToDo.Services.Handlers
{
    public class GetCategoryByIdHandler
    {
        IToDoTaskRepository _repository;

        public GetCategoryByIdHandler(IToDoTaskRepository repository)
        {
            _repository = repository;
        }

        public Category Execute(GetCategoryById command)
        {
            return _repository.GetCategoryById(command.Id);
        }
    }
}
