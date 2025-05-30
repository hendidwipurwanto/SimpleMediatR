using MediatR;
using WebApi.Models;

namespace WebApi.Features.Books.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public int Id { get; set; }
    }
}
