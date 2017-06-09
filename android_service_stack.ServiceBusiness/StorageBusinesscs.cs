using android_service_stack.ServiceBusiness.businessbase;
using android_service_stack.ServiceBusiness.model;
using android_service_stack.ServiceModel.response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceBusiness
{//入库 出库 盘点 移库 操作业务
    public class StorageBusinesscs : BaseBusiness
    {
        public NullResponse storageIn(StorageModel model)
        {
            NullResponse response = new NullResponse();
            String sqlStr;
            try
            {
                for (int i = 0; i < model.goodsList.Count(); i++)
                {
                    if (!model.goodsList[i].spill)
                    {
                        sqlStr = String.Format("update TStorageBLItem set Status='2',TallyStaffID={3},InDate=(select GETUTCDATE()) where BLID='{0}' and CompId='{1}' and LogisticsNo='{2}'", model.goodsList[i].blId, model.companyId, model.goodsList[i].logisticId, model.staffId);
                    }
                    else {
                        sqlStr = String.Format("insert into TStorageBLItem (BLTypeID,Status,BLID,CompId,LogisticsNo,CorpId,TallyStaffID,InDate) values (11,'11',NULL,'{0}','{1}','{2}','{3}',(select GETUTCDATE()))", model.companyId, model.goodsList[i].logisticId, model.corpId, model.staffId);
                    }
                    DbCommand cmd = getDbHelper().GetSqlStringCommond(sqlStr);
                    if (getDbHelper().ExecuteNonQuery(cmd) > 0)
                    {
                        sqlStr = String.Format("Select COUNT(1) from TStorageBLItem where BLID= {0} and Status= 2", model.goodsList[i].blId);
                        cmd = getDbHelper().GetSqlStringCommond(sqlStr);
                        DataTable dt = getDbHelper().ExecuteDataTable(cmd);
                        if (hasData(dt))
                        {
                            int ttlQty = Convert.ToInt32(dt.Rows[0][0]);
                            sqlStr = String.Format("Update TStorageBL set Status =3,TtlQty={1} where BLID={0} and PreTtlQty ={1}", model.goodsList[i].blId, ttlQty);
                            cmd = getDbHelper().GetSqlStringCommond(sqlStr);
                            getDbHelper().ExecuteNonQuery(cmd);
                        }
                        
                        logging();
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return response;
            }
            response.code = 200;
            response.message = "入库成功";
            return response;
        }
        public NullResponse storageOut(StorageModel model)
        {
            NullResponse response = new NullResponse();
            String sqlStr;
            try
            {
                for (int i = 0; i < model.goodsList.Count(); i++)
                {
                    sqlStr = String.Format("update TStorageBLItem set Status='4',OutDate=(select GETUTCDATE()) where BLID='{0}' and CompId='{1}' and LogisticsNo='{2}'", model.goodsList[i].blId, model.companyId, model.goodsList[i].logisticId);

                    DbCommand cmd = getDbHelper().GetSqlStringCommond(sqlStr);
                    if (getDbHelper().ExecuteNonQuery(cmd)>0)
                    {
                        sqlStr = String.Format("Select COUNT(1) from TStorageBLItem where BLID= {0} and Status= 4", model.goodsList[i].blId);
                        cmd = getDbHelper().GetSqlStringCommond(sqlStr);
                        DataTable dt = getDbHelper().ExecuteDataTable(cmd);
                        if (hasData(dt))
                        {
                            int ttlQty = Convert.ToInt32 (dt.Rows[0][0]);
                            sqlStr = String.Format("Update TStorageBL set Status =3,TtlQty={1} where BLID={0} and PreTtlQty ={1}", model.goodsList[i].blId, ttlQty);
                            cmd = getDbHelper().GetSqlStringCommond(sqlStr);
                            getDbHelper().ExecuteNonQuery(cmd);
                        }
                        logging();
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return response;
            }
            response.code = 200;
            response.message = "出库成功";
            return response;
        }
        public NullResponse storageCheck(StorageModel model)
        {
            NullResponse response = new NullResponse();
            String sqlStr;
            try
            {
                for (int i = 0; i < model.goodsList.Count(); i++)
                {
                    sqlStr = String.Format("update TStorageBLItem set Status='6',InDate=(select GETUTCDATE()) where BLID='{0}' and CompId='{1}' and LogisticsNo='{2}'", model.goodsList[i].blId, model.companyId, model.goodsList[i].logisticId);

                    DbCommand cmd = getDbHelper().GetSqlStringCommond(sqlStr);
                    if (getDbHelper().ExecuteNonQuery(cmd) > 0)
                    {
                        sqlStr = String.Format("Select COUNT(1) from TStorageBLItem where BLID= {0} and Status= 6", model.goodsList[i].blId);
                        cmd = getDbHelper().GetSqlStringCommond(sqlStr);
                        DataTable dt = getDbHelper().ExecuteDataTable(cmd);
                        if (hasData(dt))
                        {
                            int ttlQty = Convert.ToInt32(dt.Rows[0][0]);
                            sqlStr = String.Format("Update TStorageBL set Status =3,TtlQty={1} where BLID={0} and PreTtlQty ={1}", model.goodsList[i].blId, ttlQty);
                            cmd = getDbHelper().GetSqlStringCommond(sqlStr);
                            getDbHelper().ExecuteNonQuery(cmd);
                        }
                       
                        logging(); 
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return response;
            }
            response.code = 200;
            response.message = "盘点成功";
            return response;
        }
        public NullResponse storageTransform(StorageModel model)
        {
            NullResponse response = new NullResponse();
            String sqlStr;
            try
            {
                for (int i = 0; i < model.goodsList.Count(); i++)
                {
                    sqlStr = String.Format("SELECT top 1 BLItemId FROM V_LogisticsNo_Stock where status=2 and LogisticsNo='{0}'", model.goodsList[i].logisticId);
                    DbCommand cmd = getDbHelper().GetSqlStringCommond(sqlStr);
                    DataTable dt = getDbHelper().ExecuteDataTable(cmd);
                    if (hasData(dt))
                    {
                        String blItemId = dt.Rows[0][0].ToString();
                        sqlStr = String.Format("update TStorageBLItem set AreaID ={0} where CompID='{1}' and BLItemID={2}", model.areaId, model.companyId, blItemId);
                        cmd = getDbHelper().GetSqlStringCommond(sqlStr);
                        if (getDbHelper().ExecuteNonQuery(cmd) > 0)
                        { logging(); }
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                return response;
            }
            response.code = 200;
            response.message = "调拨成功";
            return response;
        }
        //TODO
        private void logging()
        { }
    }
}
