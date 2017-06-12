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
   public class StorageTransfrom : BaseBusiness
    {
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
    }

}
