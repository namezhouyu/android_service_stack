using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.response
{
  
    //盘点单列表请求返回
    public class StorageCheckBlResponse : apibase.ApiBaseResponse<StorageCheckBlResponse.StorageCheckBl>
    {
        public class StorageCheckBl
        {
           
            public List<CheckModel> models { get; set; }
            public class CheckModel
            {
                public String blId { get; set; }
                public String blNo { get; set; }
                public String staffName { get; set; }
                public String tallyDate { get; set; }

                public String remark { get; set; }
                public String areaID { get; set; }
                public String preTtlQty { get; set; }

                public String CorpSn { get; set; }
            }
        }
    }
}
