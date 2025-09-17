using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {

        protected DbContext _context;
        public EfEntityRepositoryBase(DbContext dbContext)
        {
            _context = dbContext;
        }
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            //using bloğu sayesinde işin bitince bu nesne otomatik olarak dispose (hafızadan temizleniyor, bağlantılar kapanıyor).
            //Yani context.Dispose() yazmana gerek kalmıyor.
            //Bu, IDisposable pattern dediğimiz yapıyı kullanmak demek.


                var addedEntity = _context.Entry(entity); //"Benim elimde entity isminde bir Product var, bunu veritabanına ekleyeceğim.
                addedEntity.State = EntityState.Added;  //EF Core’daki her entity’nin bir durumu (state) vardır
                _context.SaveChanges();//Yani "Added" olarak işaretlediğin Product, SQL tarafında INSERT sorgusu olarak çalışır ve veritabanına kaydedilir.
            
        }

        public void Delete(TEntity entity)
        {

                var deletedEntity = _context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                _context.SaveChanges();
            
        }

        public void Update(TEntity entity)
        {
                var updatedEntity = _context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                _context.SaveChanges();
            
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            //Bu kod filter ile belirtilen koşula uygun tek ürünü veritabanından getiriyor.Bulursa → o ürünü döner.
            //Eğer tek değil birden fazla satır gelebilecekse Where(...).ToList() kullanılır.
            //Eğer sadece tek satır bekleniyorsa SingleOrDefault veya FirstOrDefault tercih edilir.

            return _context.Set<TEntity>().SingleOrDefault(filter);



        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {

                //Set<Product>() → EF Core içinde Products tablosunu temsil eder.
                return filter == null
                    ? _context.Set<TEntity>().ToList() //Sonuç: bütün Products döner.
                    : _context.Set<TEntity>().Where(filter).ToList();//Sonuç: sadece filtreye uyan ürünler döner.Where(filter) → verilen koşula göre filtre uygular.
            

        }
    }
}
