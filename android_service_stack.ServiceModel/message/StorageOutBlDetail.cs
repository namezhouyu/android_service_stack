using android_service_stack.ServiceModel.response;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.message
{//出库单详情请求地址
    [Route("/storage/out/bl/detail", "GET")]
    public class StorageOutBlDetail : IReturn<StorageBlDetailResponse>
    {
        public String corpId { get; set; }
        public String companyId { get; set; }
    }
}
