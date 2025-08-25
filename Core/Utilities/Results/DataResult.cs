using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //DataResult<T> → Result + Data. kısacası 
    //Result’tan miras alıyor → yani Success ve Message özelliklerini kullanıyor.
    //IDataResult<T> interface’ini implemente ediyor → yani bir de Data property’si ekliyor.
    public class DataResult<T> : Result, IDataResult<T>
    {
        //Hem data, hem success bilgisi, hem de mesaj vermek istediğimizde bu constructor kullanılıyor.
        //: base(success, message) → Result’ın ilgili constructor’unu çağırıyor (Success ve Message set ediliyor).
        public DataResult(T data,bool success, string message) : base(success, message)
        {
            Data = data;    //Data=data;-> gelen data atanır.
        }

        //Mesaj vermek istemiyorsak bu constructor’u kullanıyoruz.
        //: base(success) → Success set edilir, Message boş kalır.
        public DataResult(T data, bool success) : base(success)
        {
            Data = data;   //Data = data; → gelen data atanır.
        }

        public T Data { get; } //Generic tip (T) şeklinde veri taşır.Örneğin T = Product olabilir, ya da List<Product> olabilir.
    }
}
