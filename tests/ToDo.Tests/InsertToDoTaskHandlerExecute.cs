using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using ToDo.Core.Commands;
using ToDo.Core.Models;
using ToDo.Infrastructure;
using ToDo.Services.Handlers;
using Xunit;

namespace ToDo.Tests
{
    public class InsertToDoTaskHandlerExecute
    {
        [Fact]
        public void GivenValidToDoTaskShouldIncludeInDatabase()
        {
            //Arrange
            var command = new InsertToDoTask("Study XUnit", new Category("Study"), new DateTime(2020, 12, 31));

            var options = new DbContextOptionsBuilder<DbToDoTasksContext>()
                .UseInMemoryDatabase("DbToDoTaskContext")
                .Options;
            var context = new DbToDoTasksContext(options);
            var repository = new ToDoTaskRepository(context);

            var handler = new InsertToDoTaskHandler(repository);

            //Act
            handler.Execute(command);

            //Assert
            var task = repository.GetTasks(t => t.Title.Equals(command.Title));
        }

        [Fact]
        public void WhenThrowsExceptionResultIsSuccessShouldBeFalse()
        {
            //Arrange
            var command = new InsertToDoTask("Study XUnit", new Category("Study"), new DateTime(2020, 12, 31));

            var mock = new Mock<IToDoTaskRepository>();
            mock.Setup(r => r.InsertTasks(It.IsAny<ToDoTask[]>()))
                .Throws(new Exception("Ocorreu um erro ao incluir tarefas."));
            var repository = mock.Object;

            var handler = new InsertToDoTaskHandler(repository);

            //Act
            var response = handler.Execute(command);

            //Assert
            Assert.False(response.IsSuccess);
        }
    }
}
