using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Ultilities.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate, Book>().ReverseMap();
            CreateMap<BookDtoForInsertion, Book>();
            CreateMap<Book, BookDto>();
        }
    }
}
