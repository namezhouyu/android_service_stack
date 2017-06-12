using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using android_service_stack.ServiceModel.response;

namespace android_service_stack.ServiceModel.message
{
    //获取库区列表请求地址
    [Route("/storage/areas", "GET")]
    public class StorageArea : IReturn<StorageAreasResponse>
    {
        public String companyId { get; set; }
    }
}
