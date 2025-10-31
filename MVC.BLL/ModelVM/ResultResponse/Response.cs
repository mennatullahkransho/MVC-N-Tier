using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.BLL.ModelVM.ResultResponse
{
    public record Response<T>(T Result, string? ErrorMessage, bool IsHaveErrorOrNot);
    
}
