using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using android_service_stack.ServiceModel.response;
namespace android_service_stack.ServiceModel.message
{
    //移库操作请求地址
    [Route("/storage/transform", "GET")]
    public class StorageTransform : IReturn<NullResponse>
    {
        public String storageTransformRequest { get; set; }
    }
}
