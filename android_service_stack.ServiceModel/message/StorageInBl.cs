using android_service_stack.ServiceModel.response;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.message
{//入库单列表请求地址
    [Route("/storage/in/bls", "GET")]
    public class StorageInBl : IReturn<StoragBlResponse>
    {
        public String companyId { get; set; }//公司id

        public bool plan { get; set; }//true-入库计划 false-已完成的入库单
    }
}
