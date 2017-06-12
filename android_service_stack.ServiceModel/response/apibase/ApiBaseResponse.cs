using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.response.apibase
{
    public class ApiBaseResponse<T>
    {
        public int code { get; set; }
        public String message { get; set; }
        public T data { get; set; }
     
    }
}
