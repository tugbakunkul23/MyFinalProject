using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{

    public class ErrorResult : Result  //ErrorResult, Result sınıfından miras alıyor.
    {
        //ErrorResult sınıfı, bir işlemin başarısız (false) olduğunu temsil etmek için kullanılır.
        //Yani bu sınıfı kullanırsan → Success daima false olur.
        public ErrorResult(string message) : base(false, message) //Eğer bir hata mesajı vermek istiyorsan bu constructor kullanılır.
        {

        }

        public ErrorResult() : base(false)//Eğer sadece başarısız sonucu dönmek istiyorsan (mesaj vermek istemiyorsan) bu constructor kullanılır.
        {

        }
    }
}
