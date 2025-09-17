using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Concrete.EntityFramework;



namespace DataAccess.Concrete.EntityFramework
{
    //Context:Projedeki entity class’larını (Product, Category, Customer) veritabanındaki tablolarla bağladı.
    public class NorthwindContext:DbContext
    {
        public NorthwindContext() // parametresiz constructor
        {
        }

        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-86OTNL5\SQLEXPRESS;Database=NORTHWIND;Trusted_Connection=true;TrustServerCertificate=True");
        //}

        //DbSet<T> EF Core’da bir tabloyu temsil eder.
        //projendeki Product, Category, Customer class’larını gerçek veritabanındaki tablolarla eşleştirir.
        //Bundan sonra NorthwindContext üzerinden LINQ sorguları yazıp direkt tablolar üzerinde işlem yapabileceksin


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-860TNL5\SQLEXPRESS;Database=NORTHWIND;Trusted_Connection=true;TrustServerCertificate=True");
            //}
        }


        public DbSet<Product> Products { get; set; }     //Veritabanındaki Products tablosu
        public DbSet<Category> Categories { get; set; }  //Veritabanındaki Categories tablosu
        public DbSet<Customer> Customers { get; set; }  // Veritabanındaki Customers tablosu
        public DbSet<Order> Orders { get; set; }  // Veritabanındaki Orders tablosu
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    }
}
