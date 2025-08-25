using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Constructor ile dependency injection yapıyoruz. Yani dışarıdan bir IProductService nesnesi alıyoruz.
        //Loosely Coupled (Gevşek Bağlılık) → Controller, iş katmanının somut sınıfına bağlı değil, sadece arayüzüne bağlı. Bu test ve bakım için avantaj sağlar.
        //IoC Contianer -- Inversion of control

        IProductService _productService;  //Controller, iş katmanındaki servis ile iletişim kuracak.

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]  //Bu method’un HTTP GET isteğine cevap verdiğini belirtir.
        public List<Product> Get()
        {
            //Dependency chain--bağımlılık zinciri
            var result = _productService.GetAll(); //İş katmanına gidip tüm ürünleri getirir.
            return result.Data;  //genellikle bir IDataResult<List<Product>> döner, yani hem veri hem işlem sonucu mesajı içerir. Biz sadece veriyi (Data) döndürüyoruz.






            //return new List<Product>
            //{
            //    new Product{ProductId=1, ProductName="Elma" },
            //    new Product{ProductId=2, ProductName="Armut" },
            //};
        }
    }
}
