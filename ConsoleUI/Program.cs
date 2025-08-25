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
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }
        private static void ProductTest()
        {
            //Product Test()
            ProductManager productManager = new ProductManager(new EfProductDal());

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