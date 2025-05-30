using MediatR;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Features.Books.Commands
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly AppDbContext _context;

        public CreateBookHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Author = request.Author
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}
