using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //Context:Projedeki entity class’larını (Product, Category, Customer) veritabanındaki tablolarla bağladı.
    public class NorthwindContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-86OTNL5\SQLEXPRESS;Database=NORTHWIND;Trusted_Connection=true;TrustServerCertificate=True");
        }

        //DbSet<T> EF Core’da bir tabloyu temsil eder.
        //projendeki Product, Category, Customer class’larını gerçek veritabanındaki tablolarla eşleştirir.
        //Bundan sonra NorthwindContext üzerinden LINQ sorguları yazıp direkt tablolar üzerinde işlem yapabileceksin
        public DbSet<Product> Products { get; set; }     //Veritabanındaki Products tablosu
        public DbSet<Category> Categories { get; set; }  //Veritabanındaki Categories tablosu
        public DbSet<Customer> Customers { get; set; }  // Veritabanındaki Customers tablosu
         
    }
}
