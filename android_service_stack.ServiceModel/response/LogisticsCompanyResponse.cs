using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceModel.response
{//物流公司列表返回
    public class LogisticsCompanyResponse : apibase.ApiBaseResponse<List<LogisticsCompanyResponse.Company>>
    {
      public class Company
      {
          public String corpID { get; set; }
          public String corpNo { get; set; }
          public String corpSN { get; set; }
      }
    }
}
