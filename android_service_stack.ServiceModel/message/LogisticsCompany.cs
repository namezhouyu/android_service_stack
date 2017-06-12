using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using android_service_stack.ServiceModel.response;
namespace android_service_stack.ServiceModel.message
{//获取物流公司列表请求地址
    [Route("/logistics/company", "GET")]
    public class LogisticsCompany : IReturn<LogisticsCompanyResponse>
    {
        public String companyId { get; set; }
    }
}
