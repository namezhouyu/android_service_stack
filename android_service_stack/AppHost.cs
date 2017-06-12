using Funq;
using ServiceStack;
using android_service_stack.ServiceInterface;
using System;
using android_service_stack.ServiceBusiness.businessbase;
using System.Data.Common;
using System.Data;
namespace android_service_stack
{
    //VS.NET Template Info: https://servicestack.net/vs-templates/EmptyAspNet
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("android_service_stack", typeof(MyServices).Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            DBHelper dbHelper =DBHelper.get();
            this.GlobalResponseFilters.Add((req, res, responseDto) =>
            {
                var route = req.GetRoute();
                //除去登录api，全部做token校验
                if (!route.Path.Equals("/user/login"))
                {
                    String token = req.GetHeader("token");
                    if (!token.IsEmpty())
                    {
                        DbCommand cmd = dbHelper.GetSqlStringCommond(String.Format("select 1 from Sys_UserLogOn where F_Token='{0}'", token));
                        DataSet ds = dbHelper.ExecuteDataSet(cmd);
                        if (0 == ds.Tables[0].Rows.Count)
                        {
                            res.EndRequest();
                        }
                    }
                    else {
                       res.EndRequest();
                    }
                }
                
         
            });
        }
    }
}