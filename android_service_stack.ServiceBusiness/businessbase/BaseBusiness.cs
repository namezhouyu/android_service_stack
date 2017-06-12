using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Data;

namespace android_service_stack.ServiceBusiness.businessbase
{
    public class BaseBusiness
    {
        public  DBHelper getDbHelper()
        {
            return DBHelper.get();
        }
        //查询结果是否有数据
        protected bool hasData(String sqlStr)
        { 
            DbCommand cmd = getDbHelper().GetSqlStringCommond(sqlStr);
            DataSet ds = getDbHelper().ExecuteDataSet(cmd);
            return ds.Tables[0].Rows.Count > 0;
        }
        protected bool hasData(DataSet ds)
        {
            return ds.Tables[0].Rows.Count > 0;
        }
        protected bool hasData(DataTable dt)
        {
            return dt.Rows.Count > 0;
        }
        //数据库操作是否成功
        protected bool hasDatabaseSuccess(String sqlStr)
        {
            DbCommand cmd = getDbHelper().GetSqlStringCommond(sqlStr);
            int count = getDbHelper().ExecuteNonQuery(cmd);
            return count > 0;
        }
    }
}