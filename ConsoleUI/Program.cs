using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;


namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductTest(); //yazdığımız kodu metod haline getirdik
            //IoC
            //CategoryTest();
            //DTO: Data Transformation Object

        }
        private static void CategoryTest()
        {
            NorthwindContext context = new NorthwindContext();
            EfCategoryDal categoryDal = new EfCategoryDal(context);

            CategoryManager categoryManager = new CategoryManager(categoryDal);
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }
        private static void ProductTest()
        {
            NorthwindContext context = new NorthwindContext();
            EfProductDal productDal = new EfProductDal(context);
            EfCategoryDal categoryDal = new EfCategoryDal(context);

            CategoryManager categoryManager = new CategoryManager( categoryDal);
            ProductManager productManager = new ProductManager( productDal, categoryManager);

            //Product Test()
            //ProductManager productManager = new(new EfProductDal(), new CategoryManager(new EfCategoryDal()));

            var result = productManager.GetProductDetails();

            if (result.Success==true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + "/" + product.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            //foreach (var product in productManager.GetProductDetails().Data)
            //{ 
            //    Console.WriteLine(product.ProductName + "/" + product.CategoryName); 
            //}

            
        }


    }
}