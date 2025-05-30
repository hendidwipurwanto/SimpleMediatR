using MediatR;
using WebApi.Models;

namespace WebApi.Features.Books.Commands
{
    public class CreateBookCommand : IRequest<Book>
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
