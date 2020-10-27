using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

            var mock = new Mock<ILogger<InsertToDoTaskHandler>>();

            var options = new DbContextOptionsBuilder<DbToDoTasksContext>()
                .UseInMemoryDatabase("DbToDoTaskContext")
                .Options;
            var context = new DbToDoTasksContext(options);
            var repository = new ToDoTaskRepository(context);

            var handler = new InsertToDoTaskHandler(repository, mock.Object);

            //Act
            handler.Execute(command);

            //Assert
            var task = repository.GetTasks(t => t.Title.Equals(command.Title));
        }

        delegate void GetLogMessage(LogLevel logLevel, EventId eventId, It.IsAnyType state, Exception exception, Func<It.IsAnyType, Exception, string> func);

        [Fact]
        public void GivenValidToDoTaskShouldLog()
        {
            //Arrange
            var command = new InsertToDoTask("Study XUnit", new Category("Study"), new DateTime(2020, 12, 31));

            var mockLogger = new Mock<ILogger<InsertToDoTaskHandler>>();

            LogLevel getLogLevel = LogLevel.Error;
            string getMessage = string.Empty;

            GetLogMessage getLogMessage = (logLevel, eventId, state, exception, func) =>
            {
                getLogLevel = logLevel;
                getMessage = func(state, exception);
            };

            mockLogger.Setup(l =>
                l.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                )).Callback(getLogMessage);

            var mock = new Mock<IToDoTaskRepository>();

            var handler = new InsertToDoTaskHandler(mock.Object, mockLogger.Object);

            //Act
            handler.Execute(command);

            //Assert
            Assert.Equal(LogLevel.Information, getLogLevel);
            Assert.Equal("", getMessage);
        }

        [Fact]
        public void WhenThrowsExceptionResultIsSuccessShouldBeFalse()
        {
            //Arrange
            var command = new InsertToDoTask("Study XUnit", new Category("Study"), new DateTime(2020, 12, 31));

            var mockLogger = new Mock<ILogger<InsertToDoTaskHandler>>();

            var mock = new Mock<IToDoTaskRepository>();
            mock.Setup(r => r.InsertTasks(It.IsAny<ToDoTask[]>()))
                .Throws(new Exception("Ocorreu um erro ao incluir tarefas."));
            var repository = mock.Object;

            var handler = new InsertToDoTaskHandler(repository, mockLogger.Object);

            //Act
            var response = handler.Execute(command);

            //Assert
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public void WhenThrowsExceptionShouldLogMessageException()
        {
            //Arrange
            var errorMessage = "Ocorreu um erro ao incluir tarefas.";
            var expectedException = new Exception(errorMessage);

            var command = new InsertToDoTask("Study XUnit", new Category("Study"), new DateTime(2020, 12, 31));

            var mockLogger = new Mock<ILogger<InsertToDoTaskHandler>>();

            var mock = new Mock<IToDoTaskRepository>();
            mock.Setup(r => r.InsertTasks(It.IsAny<ToDoTask[]>()))
                .Throws(expectedException);
            var repository = mock.Object;

            var handler = new InsertToDoTaskHandler(repository, mockLogger.Object);

            //Act
            var response = handler.Execute(command);

            //Assert
            mockLogger.Verify(l =>
                l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    expectedException,
                    (Func<object, Exception, string>)It.IsAny<object>()
                ),
                Times.Once());
        }
    }
}
