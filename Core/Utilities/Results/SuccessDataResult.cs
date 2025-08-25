using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //DataResult<T>’tan miras alıyor → yani Success, Message ve Data özelliklerini alıyor.
    public class SuccessDataResult<T>: DataResult<T>
    {
        //Hem data hem message gönderiliyor.Success = true otomatik.
        public SuccessDataResult(T data,string message):base(data,true,message)
        {
            
        }
        //Sadece data gönderiliyor.Success = true, Message boş kalıyor.
        public SuccessDataResult(T data):base(data,true)
        {
            
        }
        //Sadece mesaj gönderiliyor.Data gönderilmiyor → default atanıyor (null, 0, false vs. tipine göre).
        public SuccessDataResult(string message):base(default,true,message)
        {
            
        }
        //Ne data, ne mesaj gönderiliyor.Success = true, Data = default.
        public SuccessDataResult():base(default,true)
        {
            
        }
    }
}
