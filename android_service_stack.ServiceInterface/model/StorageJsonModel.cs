using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace android_service_stack.ServiceInterface.model
{//json转换实体 用于转换入库出库操作的请求json
    [DataContract]
    public class StorageJsonModel
    {
        [DataMember(Name = "goodsList")]
        public Goods[] goodsList { get; set; }
        [DataMember(Name = "staffId")]
        public int staffId { get; set; }
        [DataMember(Name = "areaId")]
        public int areaId { get; set; }
       [DataMember(Name = "corpId")]
        public String corpId { get; set; }
        [DataMember(Name = "companyId")]
        public String companyId { get; set; }
        [DataContract]
        public class Goods
        {
           [DataMember(Name = "spill")]
            public bool spill { get; set; }
           [DataMember(Name = "blId")]
           public String blId { get; set; }
            [DataMember(Name = "logisticId")]
           public String logisticId { get; set; }
            
        }

       
    }
}
