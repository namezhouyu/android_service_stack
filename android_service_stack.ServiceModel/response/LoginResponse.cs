using android_service_stack.ServiceModel.response.apibase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.response
{//登录返回
    public class LoginResponse : apibase.ApiBaseResponse<LoginResponse.User>
    {
        public class User {
            public String userId { get; set; }
            public String userToken { get; set; }
            public String userName { get; set; }

            public int staffId { get; set; }
        }
    }
}
