using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBookService
    {
        //Kodlar asenkron hale getirildi.
        Task<(IEnumerable<ExpandoObject>,MetaData metaData)> GetAllBooksAsync(BookParameters bookparameters, bool trackChanges);
        Task <BookDto> GetOneBookByIdAsync(int id, bool trackChanges);
        Task <BookDto> CreateOneBookAsync(BookDtoForInsertion book);
        Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto,bool trackChanges);
        Task DeleteOneBookAsync(int id,bool trackChanges);
        Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);
        Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate,Book book);
    }
}
