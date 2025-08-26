using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator,object entity) //Validate metodu statik olduğu için sınıf üzerinden direkt çağrılabilir.IValidator validator → Hangi doğrulama kurallarını kullanacağımızı belirtir.object entity → Doğrulanacak nesne
        {
            var context = new ValidationContext<object>(entity); //FluentValidation’da bir nesneyi doğrulamak için önce bir context oluşturmak gerekiyor.Burada entity nesnesi doğrulama için bağlanıyor.
            var result = validator.Validate(context); //validator.Validate(...) çağrısı ile kurallar işletiliyor.result → içinde doğrulamanın geçip geçmediği (IsValid) ve hatalar (Errors) var.
            if (!result.IsValid)  //Eğer IsValid == false (yani kurallara uymuyorsa),bir ValidationException fırlatılıyor.
            {
                var failures = result.Errors;
                throw new FluentValidation.ValidationException("Validation failed", failures);//Bu exception’un içine hangi kuralların çiğnendiği(result.Errors) ekleniyor.
            }
        }
    }
}
