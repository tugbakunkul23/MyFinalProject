using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //Result sınıfı, işlemin sonucunu (başarılı mı, başarısız mı) ve isteğe bağlı bir mesajı taşıyan taşıyıcı (DTO gibi) bir sınıf.
    //Result sınıfı, IResult arayüzünü (interface) somut hale getiriyor.
    public class Result : IResult
    {
        //Bu constructor iki parametre alıyor: işlem başarılı mı (success) ve mesaj (message).
        public Result(bool success, string message):this(success) //Constructor chaining (: this(success)) ile kod tekrarını azaltıyorsun.
        {
            Message = message;    //// dışarıdan gönderilen mesaj
            //Success = success;  // true
        }

        public Result(bool success)  //Mesaj vermek istemiyorsan bu kullanılıyor.
        {
            
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
