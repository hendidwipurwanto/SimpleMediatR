using MediatR;
using WebApi.Models;

namespace WebApi.Features.Books.Commands
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
