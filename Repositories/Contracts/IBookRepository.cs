﻿using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book> 
    {
        Task <PagedList<Book>> GetAllBooksAsync(BookParameters bookparameters,
            bool trackChanges); //Asenkron kod haline geldi.
        Task <Book> GetOneBookByIdAsync (int id, bool trackChanges);
        void CreateOneBook (Book book);
        void UpdateOneBook (Book book);
        void DeleteOneBook (Book book);
    }
}
