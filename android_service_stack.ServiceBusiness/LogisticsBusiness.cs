using android_service_stack.ServiceBusiness.businessbase;
using android_service_stack.ServiceModel.response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceBusiness
{
    public class LogisticsBusiness : BaseBusiness
    {
        public LogisticsCompanyResponse getLogisticsCompany(String userCompanyId)
        {
            LogisticsCompanyResponse response = new LogisticsCompanyResponse();
            String sql = String.Format("select CorpID, CorpNo, CorpSN from TCodeCorp where CompID='{0}' and IsCustomer=1", userCompanyId);
            DbCommand cmd = getDbHelper().GetSqlStringCommond(sql);
            DataTable dt = getDbHelper().ExecuteDataTable(cmd);
            List<LogisticsCompanyResponse.Company> models = new List<LogisticsCompanyResponse.Company>();
            try
            {
                if (hasData(dt))
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        LogisticsCompanyResponse.Company model = new LogisticsCompanyResponse.Company();
                        model.corpID = dr["CorpID"].ToString();
                        model.corpNo = dr["CorpNo"].ToString();
                        model.corpSN = dr["CorpSN"].ToString();
                        models.Add(model);
                    }


                }
                response.data = models;
                response.code = 200;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;

        }
    }
}
