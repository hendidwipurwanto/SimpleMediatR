using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Features.Books.Queries
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly AppDbContext _context;

        public GetAllBooksHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _context.Books.ToListAsync();
        }
    }
}
