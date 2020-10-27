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
        public IActionResult Post(Models.InsertToDoTask model)
        {
            var categoryCommand = new GetCategoryById(model.CategoryId);
            var category = new GetCategoryByIdHandler(_repository).Execute(categoryCommand);
            if (category == null)
            {
                return NotFound("Categoria não encontrada!");
            }

            var command = new InsertToDoTask(model.Title, category, model.Deadline);
            var handler = new InsertToDoTaskHandler(_repository,_logger);
            var result = handler.Execute(command);
            if (result.IsSuccess) return Ok();

            return StatusCode(500);
        }
    }
}