using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceInterface.model
{//json转换实体 用于转换入库出库操作的请求json
    public class StorageJsonModel
    {
        public List<bool> spills { get; set; }//是否溢出货

        public int staffId { get; set; }
        public String corpId { get; set; }

        public List<String> blIds { get; set; }
        public String companyId { get; set; }

        public int areaId { get; set; }
        public List<String> logisticIds { get; set; }

       
    }
}
