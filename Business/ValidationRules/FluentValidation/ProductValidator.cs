using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p=>p.ProductName).NotEmpty().WithMessage("Ürün ismi boş olamaz!");//istersen mesaj yazmayabilrisin
            RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalı!");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);//kategori 1 ise fiyat 10 dan büyük olmalı
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi İle başlamalı"); //ürün ismi A ile başlamalı
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
