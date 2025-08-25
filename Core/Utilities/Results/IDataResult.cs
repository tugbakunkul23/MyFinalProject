using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //IDataResult<T> açmamızın nedeni: işlem sonucuna ek olarak veri (data) de taşımak.
    //Yani IDataResult<T> → IResult + T Data.
    public interface IDataResult<T>:IResult
    {
        T Data { get; }  // İşlem sonucuyla birlikte dönecek veri.Data → dönen asıl veri
    }

}
