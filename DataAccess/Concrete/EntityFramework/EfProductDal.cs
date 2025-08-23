using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{   
    //NuGet
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDisposable pattern implementation of c#
            //using bloğu sayesinde işin bitince bu nesne otomatik olarak dispose (hafızadan temizleniyor, bağlantılar kapanıyor).
            //Yani context.Dispose() yazmana gerek kalmıyor.
            //Bu, IDisposable pattern dediğimiz yapıyı kullanmak demek.

            using (NorthwindContext context=new NorthwindContext())  //Burada NorthwindContext sınıfından bir nesne oluşturuyorsun.
            {
                var addedEntity = context.Entry(entity); //"Benim elimde entity isminde bir Product var, bunu veritabanına ekleyeceğim.
                addedEntity.State = EntityState.Added;  //EF Core’daki her entity’nin bir durumu (state) vardır
                context.SaveChanges();//Yani "Added" olarak işaretlediğin Product, SQL tarafında INSERT sorgusu olarak çalışır ve veritabanına kaydedilir.
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            //Bu kod filter ile belirtilen koşula uygun tek ürünü veritabanından getiriyor.Bulursa → o ürünü döner.
            //Eğer tek değil birden fazla satır gelebilecekse Where(...).ToList() kullanılır.
            //Eğer sadece tek satır bekleniyorsa SingleOrDefault veya FirstOrDefault tercih edilir.

            using (NorthwindContext context=new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
                
            }

        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                //Set<Product>() → EF Core içinde Products tablosunu temsil eder.
                return filter == null
                    ? context.Set<Product>().ToList() //Sonuç: bütün Products döner.
                    : context.Set<Product>().Where(filter).ToList();//Sonuç: sadece filtreye uyan ürünler döner.Where(filter) → verilen koşula göre filtre uygular.
            }
            
        }

        
    }
}
