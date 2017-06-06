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
        //物流公司
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
        //调拨区位
        public StorageAreasResponse getAreas(String userCompanyId)
        {
            StorageAreasResponse response = new StorageAreasResponse();
            String sql = String.Format("select * from TCodeSTArea  where CompID='{0}' ", userCompanyId);
            DbCommand cmd = getDbHelper().GetSqlStringCommond(sql);
            DataTable dt = getDbHelper().ExecuteDataTable(cmd);
            List<StorageAreasResponse.Area> models = new List<StorageAreasResponse.Area>();
            try
            {
                if (hasData(dt))
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        StorageAreasResponse.Area model = new StorageAreasResponse.Area();
                        model.areaId = dr["AreaID"].ToString();
                        model.areaNo = dr["AreaNo"].ToString();
                        model.areaName = dr["AreaCN"].ToString();
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

        public GoodsDetailResponse getGoodsDetailById(String id)
        {
            GoodsDetailResponse response = new GoodsDetailResponse();
            GoodsDetailResponse.Goods goods = new GoodsDetailResponse.Goods();
            response.data = goods;
            String sqlStr = String.Format("SELECT top 1 CorpName,GoodsName,stArea FROM V_LogisticsNo_Stock where status=2 and LogisticsNo='{0}'",id);
            DbCommand cmd = getDbHelper().GetSqlStringCommond(sqlStr);
            DataTable dt = getDbHelper().ExecuteDataTable(cmd);
            if (hasData(dt))
            { 
            goods.no = id;
            goods.area = dt.Rows[0]["stArea"].ToString();
            goods.name = dt.Rows[0]["GoodsName"].ToString();
            goods.corpSn = dt.Rows[0]["CorpName"].ToString();
            }
            return response;
        }
    }
}
