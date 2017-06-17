using System;
using ServiceStack;
using android_service_stack.ServiceModel.response;
namespace android_service_stack.ServiceModel.message
{
    //入库列表出库列表盘点列表请求地址
    [Route("/storage/bls", "GET")]
    public class StorageBl : IReturn<StoragBlResponse>
    {
        public String companyId { get; set; }

        public int type { get; set; }//1入库 2出库 
    }

}
