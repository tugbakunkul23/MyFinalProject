using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    //Burada amaç: İş kurallarını tek tek kontrol etmek yerine hepsini tek noktadan yönetebilmek.
    public class BusinessRules
    {
        //Run metodu static, yani doğrudan BusinessRules.Run(...) şeklinde çağrılabilir.
        //params IResult[] logics: Metoda birden fazla iş kuralı parametre olarak gönderilebiliyor.
        public static IResult Run(params IResult[] logics)
        {
            //logics dizisindeki her iş kuralı tek tek kontrol ediliyor.
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }

            return null;
        }
    }
}
