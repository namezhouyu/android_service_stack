using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.response
{//入库单出库单请求返回
    public class StoragBlResponse : apibase.ApiBaseResponse<StoragBlResponse.StoragePlan>
    {
        public class StoragePlan
        {
            public int preTtlQty { get; set; }//预计入库数量
            public List<PlanModel> plans {get;set;}
            public class PlanModel {
            public String blId { get; set; }//主表明细表关联
            public String blNo { get; set; }//入库id
            public String mblNo { get; set; }//提运单号
            public String corpId { get; set; }//物流企业
            public String inDate { get; set; }//入库计划日
            public String vsl { get; set; }//船名
            public String voy { get; set; }//航次
            public String carNo { get; set; }//车牌号
        }
    }
    }
}
