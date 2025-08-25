using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //Veritabanı tablolarını doğrudan dışarıya açmak yerine, sadece ihtiyacımız olan alanları taşıyan özel bir sınıftır.
    //Yani DTO = Veri taşıma sınıfı ✅
    //NuGet
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                var result = from p in context.Products   // Products tablosundaki her satırı p değişkenine al.
                             join c in context.Categories  //Products içindeki CategoryId ile Categories içindeki CategoryId eşleşen satırları birleştir.
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto    //İki tablodan gelen verilerle yeni bir DTO (Data Transfer Object) oluştur.
                             {
                                 ProductId=p.ProductId, ProductName=p.ProductName,
                                 CategoryName=c.CategoryName, UnitsInStok=p.UnitsInStock 
                             };
                return result.ToList();
            }
        }
    }
}
