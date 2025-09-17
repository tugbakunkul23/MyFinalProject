using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;



namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        //ICategoryDal _categoryDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
            //_categoryDal = categoryDal;  parantez içinde ICategoryDal categoryDal eklemiştik.
        }


        //claim
        [SecuredOperation("product.add,admin")]          //yetkiniz yok hatası için bunu kapatabilirsin
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {

            //business codes
            //validation

            //aynı isimde ürün eklenemez
            //Eğer mevcut kategori sayısı 15 i geçtiyse sisteme yeni ürün eklenemez.
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);

            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            //{
            //    if (CheckIfProductNameExists(product.ProductName).Success)
            //    {
                    
            //    }
                
            //}
            //return new ErrorResult();
            
            //ValidationTool.Validate(new ProductValidator(), product);

            
        }


        [CacheAspect]  //key,valaue
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 20)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }
        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        [CacheAspect]        //Sık kullanılan methodların sonuçlarını cache’e alır, performansı artırır.
        //[PerformansAspect(5)]          //→ Methodun çalışmasını izler, belirlediğin süreden uzun sürerse log’a düşer.5 saniyeden uzun sürerse, loglanır.
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>>  GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice <= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 20)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]

        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(product => product.CategoryId == product.CategoryId).Count;
            if (result > 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //bir kategoride en fazla 15 ürün olabilir
            //Select count(*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.CategoryId == p.CategoryId).Count;
            if (result > 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
          
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();  //any-> var mı yok mu diye bakar.
            if (result)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        //[TransactionScopeAspect]
        public IResult AddTransactionalText(Product product)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Add(product);
                    if (product.UnitPrice < 10)
                    {
                        throw new Exception("");
                    }

                    Add(product);
                    scope.Complete();

                    
                }
                catch (Exception)
                {
                    scope.Dispose();
                }
            }
            return null;

        }
    }
}
