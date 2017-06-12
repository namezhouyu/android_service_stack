using android_service_stack.ServiceModel.response;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.message
{//入库出库移库盘点操作请求地址
    [Route("/storage", "POST")]
    public class Storage : IReturn<NullResponse>
    {
        public String storageRequest { get; set; }

        public int type { get; set; }//1入库 2出库 3移库 4盘点
    }
}
