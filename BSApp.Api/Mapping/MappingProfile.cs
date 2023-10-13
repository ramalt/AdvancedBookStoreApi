using AutoMapper;
using BSApp.Entities.Dtos;
using BSApp.Entities.Dtos.User;
using BSApp.Entities.Models;

namespace BSApp.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateBookDto, Book>().ReverseMap();
        CreateMap<BookDto, Book>().ReverseMap();
        CreateMap<CreateBookDto, Book>().ReverseMap();
        CreateMap<CreateBookDto, BookDto>();

        CreateMap<RegisterUserDto, User>();
    }
}
