using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using android_service_stack.ServiceModel.response;
namespace android_service_stack.ServiceModel.message
{
   
    //盘点操作请求地址
    [Route("/storage/check", "POST")]
    public class StorageCheck : IReturn<NullResponse>
    {
        public String storageCheckRequest { get; set; }
    }
}
