using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception   //Aspect
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);  //Belirlenen validator sınıfından yeni bir validator nesnesi oluşturuyor.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];     //Bu validator hangi entity için yazılmışsa o entity tipini buluyor.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);  //Metot parametrelerinden sadece validator’ün kontrol etmesi gereken tipte olanları alıyor.
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
