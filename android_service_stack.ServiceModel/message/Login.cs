using android_service_stack.ServiceModel.response;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.message
{
   //登录请求地址
    [Route("/user/login", "POST")]
    public class Login : IReturn<LoginResponse>
    {
        public String userName { get; set; }
        public String userPassWord { get; set; }
    }
}

