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
{//物流运单列表业务
    public class StorageBlDetailBusiness : BaseBusiness
    {
        //入库物流运单列表
        public StorageBlDetailResponse getStoregeInBlDetail(String userCompanyId, String cropId)
        {
            String sql = String.Format("select * from TStorageBLItem where  CompID='{0}' and BLTypeID=11 and Status= 1 and CorpID={1}", userCompanyId, cropId);
            return getResponse(sql); ;
        }
        //出库物流运单列表
        public StorageBlDetailResponse getStoregeOutBlDetail(String userCompanyId, String cropId)
        {
            String sql = String.Format("select * from TStorageBLItem where  CompID='{0}' and BLTypeID=12 and Status= 3 and CorpID={1}", userCompanyId, cropId);
            return getResponse(sql); ;
        }
        //盘点单运单列表
        public StorageBlDetailResponse getStoregeCheckDetail(String userCompanyId, int blId)
        {
            String sql = String.Format("select * from TStorageBLItem where  CompID='{0}'and BLID={1} and Status= 5", userCompanyId, blId);
            return getResponse(sql); ;
        }
        public StorageBlDetailResponse getResponse(String sql)
        {
            StorageBlDetailResponse res = new StorageBlDetailResponse();
            DbCommand cmd = getDbHelper().GetSqlStringCommond(sql);
            DataTable dt = getDbHelper().ExecuteDataTable(cmd);
            StorageBlDetailResponse.StorageDetail detail = new StorageBlDetailResponse.StorageDetail();
            List<StorageBlDetailResponse.StorageDetail.StorageItem> items = new List<StorageBlDetailResponse.StorageDetail.StorageItem>();
            detail.items = items;
            res.data = detail;

            if (hasData(dt))
            {
                try
                {
                    
                    foreach (DataRow dr in dt.Rows)
                    {
                        StorageBlDetailResponse.StorageDetail.StorageItem item = new StorageBlDetailResponse.StorageDetail.StorageItem();
                        item.blItemId = dr["BLItemId"].ToString();
                        item.logisticsNo = dr["LogisticsNo"].ToString();
                        item.goodsName = dr["GoodsName"].ToString();
                        item.areaId = dr["AreaID"].ToString();
                        item.qty = dr["Qty"].ToString();
                        item.amt = dr["Amt"].ToString();
                        item.mblNo = dr["MBLNo"].ToString();
                        item.blNo = dr["BLNo"].ToString();
                        item.spec = dr["Specifications"].ToString();
                        if (dr["InDate"].ToString() != string.Empty)
                        {
                          item.inDate = Convert.ToDateTime(dr["InDate"]).ToString("yyyy.MM.dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                        }
                        item.blId = dr["BLID"].ToString();
                        items.Add(item);
                    }
                    detail.items = items;
                    res.data = detail;
                    res.code = 200;
                }
                catch (Exception ex)
                {
                    res.message = ex.Message;
                }
                
            }
            return res;
        }
    }
}
