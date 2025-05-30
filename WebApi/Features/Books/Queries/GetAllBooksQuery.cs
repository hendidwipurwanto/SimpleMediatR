using MediatR;
using WebApi.Models;

namespace WebApi.Features.Books.Queries
{

    public class GetAllBooksQuery : IRequest<List<Book>> 
    { 
    
    }

}
