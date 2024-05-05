using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.EF_Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EF_Core
{
    public class RepositoryContext : DbContext
    {
        // RepositoryContext için DbContextOptions nesnesi oluşturulur.Ve veritabanı bağlantısı sağlanır.
        public RepositoryContext(DbContextOptions options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        //Oluşan migrationın çekirdek data ile oluşması için tanımlanmıştır.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}
