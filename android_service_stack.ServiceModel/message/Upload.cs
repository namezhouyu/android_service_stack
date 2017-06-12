using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using android_service_stack.ServiceModel.response;
namespace android_service_stack.ServiceModel.message
{
    //上传图片请求地址
    [Route("/storage/image/upload", "POST")]
    public class Upload :IReturn<NullResponse>
    {
        public int blId { get; set; }
        public int blItemId { get; set; }

    }
}
