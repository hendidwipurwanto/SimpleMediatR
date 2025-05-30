using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Features.Books.Commands;
using WebApi.Features.Books.Queries;
using WebApi.Models;

namespace UnitTest
{
    public class BooksControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BooksController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnBooksList()
        {
            // Arrange
            var books = new List<Book>
        {
            new Book { Id = 1, Title = "Book A", Author = "Author A" }
        };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllBooksQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(books);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBooks = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Assert.Single(returnBooks);
        }

        [Fact]
        public async Task GetById_ShouldReturnBook_WhenExists()
        {
            var book = new Book { Id = 1, Title = "Book A", Author = "Author A" };
            _mediatorMock.Setup(m => m.Send(It.Is<GetBookByIdQuery>(q => q.Id == 1), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(book);

            var result = await _controller.Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBook = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(1, returnBook.Id);
        }
        
        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenNotExists()
        {
            _mediatorMock.Setup(m => m.Send(It.Is<GetBookByIdQuery>(q => q.Id == 99), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((Book)null);

            var result = await _controller.Get(99);

            Assert.IsType<NotFoundResult>(result);
        } 

        [Fact]
        public async Task Create_ShouldReturnCreatedBook()
        {
            var createCommand = new CreateBookCommand { Title = "Book A", Author = "Author A" };
            var createdBook = new Book { Id = 1, Title = "Book A", Author = "Author A" };

            _mediatorMock.Setup(m => m.Send(createCommand, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(createdBook);

            var result = await _controller.Create(createCommand);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBook = Assert.IsType<Book>(okResult.Value);
            Assert.Equal("Book A", returnBook.Title);
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedBook_WhenIdMatches()
        {
            var command = new UpdateBookCommand { Id = 1, Title = "Updated", Author = "Updated" };
            var updated = new Book { Id = 1, Title = "Updated", Author = "Updated" };

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(updated);

            var result = await _controller.Update(1, command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBook = Assert.IsType<Book>(okResult.Value);
            Assert.Equal("Updated", returnBook.Title);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenIdMismatch()
        {
            var command = new UpdateBookCommand { Id = 2, Title = "Updated", Author = "Updated" };

            var result = await _controller.Update(1, command);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID tidak cocok.", badRequest.Value);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenSuccess()
        {
            _mediatorMock.Setup(m => m.Send(It.Is<DeleteBookCommand>(c => c.Id == 1), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenFailed()
        {
            _mediatorMock.Setup(m => m.Send(It.Is<DeleteBookCommand>(c => c.Id == 99), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(false);

            var result = await _controller.Delete(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
