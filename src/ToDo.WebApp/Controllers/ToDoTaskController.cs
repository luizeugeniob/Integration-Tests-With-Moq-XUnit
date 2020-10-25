using Microsoft.AspNetCore.Mvc;
using ToDo.Core.Commands;
using ToDo.Services.Handlers;

namespace ToDo.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        [HttpPost]
        public IActionResult EndpointCadastraTarefa(Models.InsertToDoTask model)
        {
            var categoryCommand = new GetCategoryById(model.CategoryId);
            var categoria = new GetCategoryByIdHandler().Execute(categoryCommand);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada!");
            }

            var taskCommand = new InsertToDoTask(model.Title, categoria, model.Deadline);
            var handler = new InsertToDoTaskHandler();
            handler.Execute(taskCommand);
            return Ok();
        }
    }
}