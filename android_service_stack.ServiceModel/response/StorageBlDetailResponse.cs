using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.response
{//入库单详情出库单详情请求返回
    public class StorageBlDetailResponse : apibase.ApiBaseResponse<StorageBlDetailResponse.StorageDetail>
    {
        public class StorageDetail
        {
            public List<StorageItem> items { get; set; }
            public int toScanningCount { get; set; }//待扫描数
            public class StorageItem
            {
                public String mblNo { get; set; }//提单号
                public String blNo { get; set; }//入库单号
                public String inDate { get; set; }//计划入库日期
                public String spec { get; set; }//规格
                public String blId { get; set; }//主表主键，入库需要
                public String blItemId { get; set; }//序号
                public String logisticsNo { get; set; }//物流运单编号
                public String goodsName { get; set; }//货物名称
                public String areaId { get; set; }//库位ID
                public String qty { get; set; }//数量
                public String amt { get; set; }//货值

                public String corpSn { get; set; }//物流公司

               
            }
        }
    }
}
