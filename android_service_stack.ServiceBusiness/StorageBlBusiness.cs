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
{//主单据列表业务
    public class StorageBlBusiness : BaseBusiness
    {
        public StoragBlResponse getStorageInBls(String companyId)
        {
            String sql = "";
            //if (plan)
            //{ //入库计划
            //    sql = String.Format("select * from TStorageBL where BLTypeID='11' and Status='2' and CompID='{0}'", companyId);
            //}
            //else
            //{
            //    //入库单列表
            //    sql = String.Format("select * from TStorageBL where BLTypeID='11' and (Status='3' or Status='4') and CompID='{0}'", companyId);
            //}
            //入库单列表
            sql = String.Format("select * from TStorageBL where BLTypeID='11' and Status> 1 and CompID='{0}'", companyId);
            return getStoragBlResponse(sql);

        }
		public StoragBlResponse getStorageOutBls(String companyId)
		{
			String sql = "";
			//出库单列表
			sql = String.Format("select * from TStorageBL where BLTypeID='12' and Status> 1 and CompID='{0}'", companyId);
            return getStoragBlResponse(sql);

		}
        //计划盘点列表
        public StorageCheckBlResponse getStorageCheckPlanBls(String companyId, int staffId)
        {
            String sql = String.Format("Select  BLNo,BLID,  t1.staffName, s.TallyDate from TStorageBL s Left join Teistaff t1 on (t1.StaffId=s.OpID) where s.CompID='{0}' and BLTypeID= 14 and Status=2 and StaffID={1}", companyId, staffId);
            return getStorageCheckBlResponse(sql);
        }
        //盘点历史列表
        public StorageCheckBlResponse getStorageCheckHistoryBls(String companyId, int staffId)
        {
            String sql = String.Format("Select  BLNo,BLID,  t1.staffName, s.TallyDate from TStorageBL s Left join Teistaff t1 on (t1.StaffId=s.OpID) where s.CompID='{0}' and BLTypeID= 14 and Status>2 and StaffID={1}", companyId, staffId);
            return getStorageCheckBlResponse(sql);
        }
        public StorageCheckBlResponse getStorageCheckBlResponse(String sql)
        {
            StorageCheckBlResponse response = new StorageCheckBlResponse();
            DbCommand cmd = getDbHelper().GetSqlStringCommond(sql);
            DataTable dt = getDbHelper().ExecuteDataTable(cmd);
            List<StorageCheckBlResponse.StorageCheckBl.CheckModel> models = new List<StorageCheckBlResponse.StorageCheckBl.CheckModel>();
            StorageCheckBlResponse.StorageCheckBl storegeCheckBl = new StorageCheckBlResponse.StorageCheckBl();
            response.data = storegeCheckBl;
            storegeCheckBl.models = models;
            if (hasData(dt))
            {
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        StorageCheckBlResponse.StorageCheckBl.CheckModel model = new StorageCheckBlResponse.StorageCheckBl.CheckModel();
                        model.blId = dr["BLID"].ToString();
                        model.blNo = dr["BLNo"].ToString();
                        model.staffName = dr["staffName"].ToString();
                        if (dr["TallyDate"].ToString() != string.Empty)
                        {
                            model.tallyDate = Convert.ToDateTime(dr["TallyDate"]).ToString("yyyy.MM.dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                        }

                        models.Add(model);
                    }
                    response.code = 200;
                }
                catch (Exception ex)
                {
                    response.message = ex.Message;
                }

            }

            return response;
        }
        public StoragBlResponse getStoragBlResponse(String sql)
        {
            StoragBlResponse response = new StoragBlResponse();
            DbCommand cmd = getDbHelper().GetSqlStringCommond(sql);
            DataTable dt = getDbHelper().ExecuteDataTable(cmd);

            List<StoragBlResponse.StoragePlan.PlanModel> models = new List<StoragBlResponse.StoragePlan.PlanModel>();
            StoragBlResponse.StoragePlan storagePlan = new StoragBlResponse.StoragePlan();

            if (hasData(dt))
            {
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        StoragBlResponse.StoragePlan.PlanModel model = new StoragBlResponse.StoragePlan.PlanModel();
                        model.blId = dr["BLID"].ToString();
                        model.blNo = dr["BLNo"].ToString();
                        model.mblNo = dr["MBLNo"].ToString();
                        model.vsl = dr["Vsl"].ToString();
                        model.voy = dr["Voy"].ToString();
                        model.corpId = dr["CorpId"].ToString();
                        model.carNo = dr["CarNo"].ToString();
                        if (dr["InDate"].ToString() != string.Empty)
                        {
                            model.inDate = Convert.ToDateTime(dr["InDate"]).ToString("yyyy.MM.dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                        }
                        //storagePlan.preTtlQty += Convert.ToInt32(dr["PreTtlQty"]);
                        models.Add(model);
                    }
                    storagePlan.plans = models;

                    response.code = 200;
                    response.data = storagePlan;
                }
                catch (Exception ex)
                {
                    response.message = ex.Message;
                }

            }
            return response;
        }

    }

}
