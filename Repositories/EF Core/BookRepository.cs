using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EF_Core
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {

        }
        public void CreateOneBook(Book book)=>Create(book);
        public void DeleteOneBook(Book book)=>Delete(book);
        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookparameters,bool trackChanges)
        {
            var books = await FindAll(trackChanges)
                .FilterBooks(bookparameters.MinPrice,bookparameters.MaxPrice)
                .Search(bookparameters.SearchTerm)
                .Sort(bookparameters.OrderBy)
                .ToListAsync(); //Asenkron dönmesi için ekledik.
            
            return PagedList<Book>
                .ToPagedList(books, 
                bookparameters.PageNumber,
                bookparameters.PageSize);
        }
        public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
        public void UpdateOneBook(Book book)=> Update(book);
    }
}
