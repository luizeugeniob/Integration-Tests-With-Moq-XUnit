using Moq;
using ToDo.Core.Commands;
using ToDo.Infrastructure;
using ToDo.Services.Handlers;
using Xunit;

namespace ToDo.Tests
{
    public class GetCategoryByIdHandlerExecute
    {
        [Fact]
        public void WhenExistsIdShouldInvokeGetCategoryByIdOnlyOnceTime()
        {
            //Arrange
            var categoryId = 20;
            var command = new GetCategoryById(categoryId);

            var mock = new Mock<IToDoTaskRepository>();

            var repository = mock.Object;

            var handler = new GetCategoryByIdHandler(repository);

            //Act
            handler.Execute(command);

            //Assert
            mock.Verify(r => r.GetCategoryById(categoryId), Times.Once());
        }
    }
}
