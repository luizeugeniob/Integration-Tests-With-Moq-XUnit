using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<InsertToDoTaskHandler> _logger;

        public ToDoTaskController(IToDoTaskRepository repository, ILogger<InsertToDoTaskHandler> logger)
        {
            _repository = repository;
            _logger = logger;
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
            var handler = new InsertToDoTaskHandler(_repository,_logger);
            handler.Execute(taskCommand);
            return Ok();
        }
    }
}