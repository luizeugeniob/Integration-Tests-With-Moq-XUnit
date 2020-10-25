using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Core.Commands;
using ToDo.Core.Models;
using ToDo.Infrastructure;
using ToDo.Services.Handlers;
using Xunit;

namespace ToDo.Tests
{
    public class ToDoTaskDeadlineManagerHandlerExecute
    {
        [Fact]
        public void WhenToDoTasksAreDelayedShouldChangeStatus()
        {
            //Arrange
            var category1 = new Category("Category1");
            var category2 = new Category("Category2");
            var category3 = new Category("Category3");
            var category4 = new Category("Category4");
            var category5 = new Category("Category5");

            var tasks = new List<ToDoTask>
            {
                //Atrasadas
                new ToDoTask(10, "Task 1", category1, new DateTime(2019, 9, 28), null, ToDoTaskStatus.Created),
                new ToDoTask(2, "Task 2", category2, new DateTime(2019, 8, 12), null, ToDoTaskStatus.Pending),
                new ToDoTask(3, "Task 3", category3, new DateTime(2019, 7, 19), null, ToDoTaskStatus.Created),
                new ToDoTask(4, "Task 4", category4, new DateTime(2019, 6, 6), null, ToDoTaskStatus.Pending),
                //Dentro do prazo                                       
                new ToDoTask(5, "Task 5", category5, new DateTime(2019, 5, 30), null, ToDoTaskStatus.Completed),
                new ToDoTask(6, "Task 6", category1, new DateTime(2020, 10, 21), null, ToDoTaskStatus.Created),
                new ToDoTask(7, "Task 7", category2, new DateTime(2020, 6, 10), null, ToDoTaskStatus.Created),
                new ToDoTask(8, "Task 8", category3, new DateTime(2020, 10, 12), null, ToDoTaskStatus.Pending),
                new ToDoTask(9, "Task 9", category4, new DateTime(2020, 1, 14), null, ToDoTaskStatus.Created)
            };

            var options = new DbContextOptionsBuilder<DbToDoTasksContext>()
                .UseInMemoryDatabase("DbToDoTaskContext")
                .Options;
            var context = new DbToDoTasksContext(options);
            var repository = new ToDoTaskRepository(context);

            repository.InsertTasks(tasks.ToArray());

            var command = new ToDoTaskDeadlineManager(new DateTime(2020, 1, 1));
            var handler = new ToDoTaskDeadlineManagerHandler(repository);

            //Act
            handler.Execute(command);

            //Assert
            var delayedTasks = repository.GetTasks(t => t.Status == ToDoTaskStatus.Delayed);
            Assert.Equal(4, delayedTasks.Count());
        }
    }
}
