using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        //Hem data hem message gönderiliyor.Success = false otomatik atanır..
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }
        //Sadece data gönderiliyor.Message boş kalır, Success = false.
        public ErrorDataResult(T data) : base(data, false)
        {

        }
        //Sadece mesaj gönderiliyor.Data default atanır (null, 0, false vs. tipine göre).Success = false.
        public ErrorDataResult(string message) : base(default, false, message)
        {

        }
        //Ne data, ne mesaj gönderiliyor.Success = false, Data = default.
        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
