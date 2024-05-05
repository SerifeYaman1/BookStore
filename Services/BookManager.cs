using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager:IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<BookDto>_shaper;

        public BookManager(IRepositoryManager manager, 
            ILoggerService logger, 
            IMapper mapper,
            IDataShaper<BookDto> shaper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _shaper = shaper;
        }
        public async Task <BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {
            var entity = _mapper.Map<Book>(bookDto);
            _manager.Book.CreateOneBook(entity);
            await _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);  
        }
        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            var entity= await GetOneBookByIdAndCheckExits(id, trackChanges);
            _manager.Book.DeleteOneBook(entity);
            await _manager.SaveAsync();
        }
        public async Task <(IEnumerable<ExpandoObject>,MetaData metaData)> 
            GetAllBooksAsync(BookParameters bookparameters, bool trackChanges)
        {
            if(!bookparameters.ValidPriceRange)
                throw new PriceOutofRangeBadRequestException();

            var booksWithMetaData= await _manager
                .Book
                .GetAllBooksAsync(bookparameters,trackChanges);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);

            var shapedData = _shaper.ShapeData(booksDto,bookparameters.Fields);
            return (shapedData,metaData: booksWithMetaData.MetaData);
        }
        public async Task <BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookByIdAndCheckExits(id,trackChanges);
            return _mapper.Map<BookDto>(book);
        }
        public async Task <(BookDtoForUpdate bookDtoForUpdate, Book book)> 
            GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookByIdAndCheckExits(id, trackChanges);
            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);
            return(bookDtoForUpdate,book);
        }
        public async Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            _mapper.Map(bookDtoForUpdate, book);
            await _manager.SaveAsync();
        }
        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            //check entity
            var entity=await GetOneBookByIdAndCheckExits(id,trackChanges);
            //Mapping
            entity = _mapper.Map<Book>(bookDto);
            _manager.Book.Update(entity);
            await _manager.SaveAsync();
        }
        private async Task<Book> GetOneBookByIdAndCheckExits(int id,bool trackChanges)
        {
            //check entity 
            var entity = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
            if (entity is null)
            {
                throw new BookNotFoundException(id);
            }
            return entity;
        }
    }
}
