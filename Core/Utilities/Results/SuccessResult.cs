using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result    //SuccessResult, Result sınıfının özelliklerini ve davranışlarını miras alıyor.
    {
        public SuccessResult(string message) : base(true, message) //Bu constructor mesajlı kullanım için.: base(true, message) → Result sınıfının constructor’unu çağırıyor:
        {

        }

        public SuccessResult() : base(true)   //Bu constructor mesajsız kullanım için.
        {

        }
    }
}
