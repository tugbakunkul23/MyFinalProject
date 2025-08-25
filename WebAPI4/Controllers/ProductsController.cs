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

        [HttpGet("getall")]  //Bu method’un HTTP GET isteğine cevap verdiğini belirtir.
        public IActionResult Get()
        {
            //Dependency chain--bağımlılık zinciri
            var result = _productService.GetAll(); //İş katmanına gidip tüm ürünleri getirir.

            if (result.Success)
            {
                return Ok(result.Data);  // Başarılıysa 200 OK + ürün listesini döndürür
            }

            return BadRequest(result.Message); // Başarısızsa 400 BadRequest + hata mesajını döndürür

            //return result.Data;  //genellikle bir IDataResult<List<Product>> döner, yani hem veri hem işlem sonucu mesajı içerir. Biz sadece veriyi (Data) döndürüyoruz.




            //return new List<Product>
            //{
            //    new Product{ProductId=1, ProductName="Elma" },
            //    new Product{ProductId=2, ProductName="Armut" },
            //};
        }

        [HttpGet("getbyid")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult Post(Product product)  //Product product → Gönderilen ürün bilgisi burada parametre olarak alınır.
        {
            var result = _productService.Add(product);  //İş katmanında ürünü ekleme işlemi yapılır.
            if (result.Success)
            {
                return Ok(result);   //eğer başaşrılıysa 200 OK döner.
            }
            return BadRequest(result);  //başarısızsa 400 Bad Request ve hata detayını döner.
        }


    }
}
