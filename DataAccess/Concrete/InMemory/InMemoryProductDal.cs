using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory  //“Veri erişim” katmanının somut ve in-memory (RAM) implementasyonu.
{
    //InMemoryProductDal--> sanki bir datamız varmışta biz onu yönetiyor olacaz.dolayısıyla bu proje başladığında sanki veri varmış gibi ortamı simüle edelim.
    public class InMemoryProductDal : IProductDal  //Bu sınıf, IProductDal arayüzündeki sözleşmeyi (metod imzalarını) uygulamak zorunda.
    {
        //veri varmış gibi davranacağımız için bir ürün listesi oluşturalım
        List<Product> _products;   //Bu liste, ürünleri bellekte tutar (gerçek DB yerine)._products-->bellekte tuttuğum ürün listesi

        //Kurucu (constructor) ve başlangıç verisi
        public InMemoryProductDal()
        {
            //Sınıf oluşturulurken 5 ürün ekleniyor; böylece “DB’de veri varmış gibi” davranabiliyoruz.
            //sizin yerinize bellekte bir ürün oluşturdu.
            _products = new List<Product> {
                 new Product{ProductId=1, CategoryId=1, ProductName="Bardak", UnitPrice=15, UnitsInStock=15},
                 new Product{ProductId=2, CategoryId=1, ProductName="Kamera", UnitPrice=500, UnitsInStock=3},
                 new Product{ProductId=3, CategoryId=2, ProductName="Telefon", UnitPrice=1500, UnitsInStock=2},
                 new Product{ProductId=4, CategoryId=2, ProductName="Klavye", UnitPrice=150, UnitsInStock=65},
                 new Product{ProductId=5, CategoryId=2, ProductName="Fare", UnitPrice=85, UnitsInStock=1}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //Product productToDelete = null; //şimdilik null diyelim
            //foreach(var p in _products)        //foreach ile listedeki her ürün (p) dolaşılır.
            //{
            //    if (product.ProductId == p.ProductId) //Parametre olarak gönderilen ürünün kimliği (ProductId) ile listedeki ürünün kimliği eşit mi diye bakar.
            //    {
            //        productToDelete = p;   //Eşitse, o listedeki nesneyi productToDelete değişkenine atayarak referansını tutar.
            //    }
            //}


            //LİNQ -Language Integrated Query onun yerine linq sorgusuyla yazma
            //.SingleOrDefault(...)-->LINQ’un  bir uzantı (extension) metodu.İçine verdiğin koşula (predicate) uyan elemanı bulur.
            //p => p.ProductId == product.ProductId-->Bu bir lambda ifadesi.p listedeki her bir ürün. => “şu koşulu uygula” demek.
            //p.ProductId == product.ProductId: Listedeki ürünün ID’si, dışarıdan gelen ürünün ID’sine eşitse true döner.
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            //Sonra _products.Remove(productToDelete); diyerek listeden çıkarıyorsun.
            _products.Remove(productToDelete); //döngü bittiğinde çağrılır

        }
        //Neden Remove(product) demiyoruz?
        //Çünkü dışarıdan gelen product aynı referans olmayabilir.Sen kimliğe göre(id) bulup, listedeki gerçek nesnenin referansını kaldırmak istiyorsun

        public void Update(Product product)
        {
            //Güncellemek istediğim ürünle aynı ID’ye sahip ürünü bul demek.Bulduğun ürünü productToUpdate değişkenine atıyorsun.
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;

        }

        public List<Product> GetAll()   //veriabanındaki datayı businesse vermem lazım.business ürün listesi istediğinde ona vermek zorundasın
        {
            return _products;
        }

        public List<Product> GetAllByCategory(int CategoryId)
        {
            return _products.Where(p => p.CategoryId == CategoryId).ToList();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
