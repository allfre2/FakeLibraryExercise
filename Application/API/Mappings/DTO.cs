using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Security;

namespace Application.Mappings;

public class DTO : Profile
{
    public DTO()
    {
        CreateMap<Author, AuthorDTO>();
        CreateMap<Book, BookDTO>();
        CreateMap<Loan, LoanDTO>();

        CreateMap<RegisteredClient, RegisteredClientDTO>();
        CreateMap<AccessToken, AccessTokenDTO>();
    }
}
