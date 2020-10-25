using Microsoft.AspNetCore.Mvc;
using ToDo.Core.Commands;
using ToDo.Infrastructure;
using ToDo.Services.Handlers;

namespace ToDo.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly IToDoTaskRepository _repository;

        public ToDoTaskController(IToDoTaskRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult EndpointCadastraTarefa(Models.InsertToDoTask model)
        {
            var categoryCommand = new GetCategoryById(model.CategoryId);
            var categoria = new GetCategoryByIdHandler(_repository).Execute(categoryCommand);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada!");
            }

            var taskCommand = new InsertToDoTask(model.Title, categoria, model.Deadline);
            var handler = new InsertToDoTaskHandler(_repository);
            handler.Execute(taskCommand);
            return Ok();
        }
    }
}