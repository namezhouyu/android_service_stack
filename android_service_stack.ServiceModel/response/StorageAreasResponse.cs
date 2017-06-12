using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.response
{
    //库区列表返回
    public class StorageAreasResponse : apibase.ApiBaseResponse<List<StorageAreasResponse.Area>>
    {
        public class Area
        {
            public String areaId { get; set; }
            public String areaName { get; set; }
            public String areaNo { get; set; }
        }
    }
}
