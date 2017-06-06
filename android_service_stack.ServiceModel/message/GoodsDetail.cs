using android_service_stack.ServiceModel.response;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.message
{
    //通过id获取货物信息
    [Route("/goods/detail", "GET")]
    public class GoodsDetail : IReturn<GoodsDetailResponse>
    {
        public String goodsId { get; set; }
    }
}
