using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceBusiness.model
{
   public class StorageModel
    {
       
        public Goods[] goodsList { get; set; }
       
        public int staffId { get; set; }
        
        public int areaId { get; set; }
      
        public String corpId { get; set; }
       
        public String companyId { get; set; }
        
        public class Goods
        {
         
            public bool spill { get; set; }
         
            public String blId { get; set; }
            
            public String logisticId { get; set; }

        }

    }
}
