using MediatR;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Features.Books.Commands
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly AppDbContext _context;

        public UpdateBookHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.Id);
            if (book == null)
            {
                return null;
            }

            book.Title = request.Title;
            book.Author = request.Author;

            await _context.SaveChangesAsync();
            return book;
        }
    }
}
