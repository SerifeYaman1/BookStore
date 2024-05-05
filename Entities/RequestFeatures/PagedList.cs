using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class PagedList<T> :List<T>
    {
        //ctor
        public PagedList(List<T> items, int count, int pageNumber,int pageSize)
        {
            MetaData = new MetaData()
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPage = (int)Math.Ceiling(count/(double)pageSize) //tam sayı olmalı
            };
            AddRange(items); //List<T> gelen değerleri PagedList'e taşımış oluruz.
        }
        public MetaData MetaData { get; set; }
        public static PagedList<T> ToPagedList(IEnumerable<T> source,
           int pageNumber,
           int pageSize) 
        {
            var count = source.Count();
            var items = source
                .Skip((pageNumber - 1) * pageSize) //Atlamamız gereken kayıt sayısı
                .Take(pageSize) // Kaç kayıt almamız gerektiğini söyleriz.
                .ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);      
        }
    }
}
