using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using android_service_stack.ServiceModel.response;
namespace android_service_stack.ServiceModel.message
{
   
    //盘点单计划列表请求地址
    [Route("/storage/check/bls", "GET")]
    public class StorageCheckBl : IReturn<StoragBlResponse>
    {
        public String companyId { get; set; }//公司id
        public int staffId { get; set; }

        public int type { get; set; }
    }
}
