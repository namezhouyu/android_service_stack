using android_service_stack.ServiceModel.response;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.message
{//入库单详情请求地址
    [Route("/storage/in/bl/detail", "GET")]
    public class StorageInBlDetail : IReturn<StorageBlDetailResponse>
    {
        public String corpId { get; set; }
        public String companyId { get; set; }
    }
}
