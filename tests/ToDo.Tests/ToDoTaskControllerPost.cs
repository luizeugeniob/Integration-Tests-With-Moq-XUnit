using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using ToDo.Core.Models;
using ToDo.Infrastructure;
using ToDo.Services.Handlers;
using ToDo.WebApp.Controllers;
using ToDo.WebApp.Models;
using Xunit;

namespace ToDo.Tests
{
    public class ToDoTaskControllerPost
    {
        [Fact]
        public void GivenValidToDoTaskShouldReturnOkResult()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<InsertToDoTaskHandler>>();

            var options = new DbContextOptionsBuilder<DbToDoTasksContext>()
                .UseInMemoryDatabase("DbToDoTaskContext")
                .Options;
            var context = new DbToDoTasksContext(options);

            context.Categories.Add(new Category(20, "Study"));
            context.SaveChanges();

            var repository = new ToDoTaskRepository(context);

            var controller = new ToDoTaskController(repository, mockLogger.Object);
            var model = new InsertToDoTask
            {
                CategoryId = 20,
                Title = "Study XUnit",
                Deadline = new DateTime(2020, 12, 31)
            };

            //Act
            var result = controller.Post(model);

            //Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
