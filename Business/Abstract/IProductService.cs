using Entities.Concrete;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    //iş katmanında kullanacağımız  servis operasyonları
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetAllByCategoryId(int İd);
        List<Product> GetByUnitPrice(decimal min, decimal max);
        List<ProductDetailDto> GetProductDetails();
    }
}
