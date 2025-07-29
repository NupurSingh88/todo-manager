using Moq;

namespace TodoList.Api.Application.Tests
{
    public class TodoServiceTests
    {
        private readonly Mock<ITodoRepository> _todoRepoMock;
        private readonly TodoService _todoService;

        public TodoServiceTests()
        {
            _todoRepoMock = new Mock<ITodoRepository>();
            _todoService = new TodoService(_todoRepoMock.Object);
        }

        [Fact]
        public async Task CreateTodoItemAsync_ShouldCallRepositoryWithCorrectData()
        {
            // Arrange
            var request = new CreateTodoItemRequest
            {
                Name = "Test Task",
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            // Act
            var result = await _todoService.CreateTodoItemAsync(request);

            // Assert
            _todoRepoMock.Verify(repo => repo.CreateTodoItemAsync(It.Is<TodoItem>(
                item => item.Name == request.Name && item.DueDate == request.DueDate)), Times.Once);

            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.DueDate, result.DueDate);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task CreateTodoItemAsync_ShouldReturnExpectedResponse()
        {
            // Arrange
            var request = new CreateTodoItemRequest
            {
                Name = "Important task",
                DueDate = new DateTime(2025, 1, 1)
            };

            // Act
            var result = await _todoService.CreateTodoItemAsync(request);

            // Assert
            Assert.Equal("Important task", result.Name);
            Assert.Equal(new DateTime(2025, 1, 1), result.DueDate);
        }

        [Fact]

        public async Task CreateTodoItemAsync_ShouldReturnExceptionIfNameIsNull()
        {

            // Arrange
            var request = new CreateTodoItemRequest
            {
                DueDate = new DateTime(2025, 12, 1)
            };
            

            
            //Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _todoService.CreateTodoItemAsync(request));
        }
    }
}
