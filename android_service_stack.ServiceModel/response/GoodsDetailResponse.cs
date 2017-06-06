using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.response
{
    public class GoodsDetailResponse : apibase.ApiBaseResponse<GoodsDetailResponse.Goods>
    {
        public class Goods
        {
            public String no { get; set; }
            public String area { get; set; }
            public String name { get; set; }

            public String corpSn { get; set; }
        }
    }
}
