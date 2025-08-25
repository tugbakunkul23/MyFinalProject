using Core.Utilities.Results;
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
        //IDataResult hem data hemde işlem sonucu döndürüyor ve hepsini buna çevirdik.
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int İd);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);   //void yerine IResult yaptık ki işlem sonucu dönebilelim.
    }
}
