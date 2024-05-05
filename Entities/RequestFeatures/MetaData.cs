using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class MetaData
    {
        public int CurrentPage { get; set; } //Geçerli sayfa
        public int TotalPage { get; set; } // Toplam Sayfa Sayısı
        public int PageSize { get; set; } // Gösterilecek kayıt sayısı
        public int TotalCount { get; set; } //Toplam kayıt sayısı

        // Bu sayfadan önce bir sayfa var mı kontrolü yapılır.
        public bool HasPrevious => CurrentPage > 1; // CurrentPage 1 'den büyükse vardır.
        public bool HasNextPage => CurrentPage < TotalPage; // Sonrasında sayfa vardır.
    }
}
