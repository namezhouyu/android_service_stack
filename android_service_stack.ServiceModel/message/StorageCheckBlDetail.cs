using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using android_service_stack.ServiceModel.response;
namespace android_service_stack.ServiceModel.message
{
   
    //盘点详情请求地址
    [Route("/storage/check/bl/detail", "GET")]
    public class StorageCheckBlDetail : IReturn<StorageBlDetailResponse>
    {
        public int blId { get; set; }
        public String companyId { get; set; }
    }
}
