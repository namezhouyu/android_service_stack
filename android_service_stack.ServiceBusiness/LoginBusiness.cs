using android_service_stack.ServiceBusiness.businessbase;
using android_service_stack.ServiceModel.response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace android_service_stack.ServiceBusiness
{
    public class LoginBusiness :BaseBusiness
    {
        /**用户登录
         *@param userName   用户名
         *@parem passWord 用户密码
         */
        public LoginResponse login(String userName,String userPassWord)
        {
            LoginResponse res = new LoginResponse();
            //通过用户名查询用户的基本信息
            String sql = String.Format("select a.F_Id ,a.F_Account,b.F_UserId,b.F_UserSecretkey,b.F_UserPassword from Sys_User a,Sys_UserLogOn b where a.F_Id=b.F_UserId and a.F_Account='{0}'", userName);
            DbCommand cmd = getDbHelper().GetSqlStringCommond(sql);
            DataTable dt = getDbHelper().ExecuteDataTable(cmd);
            if (hasData(dt)){
                String name = dt.Rows[0]["F_Account"].ToString();
                String id = dt.Rows[0]["F_UserId"].ToString();
                String pass = dt.Rows[0]["F_UserPassWord"].ToString();
                String userSecret = dt.Rows[0]["F_UserSecretkey"].ToString();
                
                String passWord = md5(Encrypt(userPassWord, userSecret).ToLower(),32).ToLower();
                if (passWord.Equals(pass))
                {
                    String token = Guid.NewGuid().ToString();
                    String sqlUpdateToken = String.Format("update Sys_UserLogOn set F_Token = '{0}' where F_UserId='{1}' and F_UserPassWord='{2}'", token, id, pass);
                    if (hasDatabaseSuccess(sqlUpdateToken))
                    {
                        sql = String.Format("select StaffId from TEIStaff where StaffCode='{0}'", userName);
                        cmd = getDbHelper().GetSqlStringCommond(sql);
                        dt= getDbHelper().ExecuteDataTable(cmd);
                        int statffId = Int32.Parse(dt.Rows[0]["StaffId"].ToString());

                        LoginResponse.User user = new LoginResponse.User();
                        user.userId = id;
                        user.userName = name;
                        user.staffId = statffId;
                        user.userToken = token;
                        res.data = user;
                        res.code = 200;
                        res.message = "登录成功";
                    }
                }
                else
                {
                    res.message = "密码错误";
                }
            }
            else {
                res.message = "不存在的用户";
            }
            return res;

        }
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        public static string md5(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            }

            if (code == 32)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }

            return strEncrypt;
        }
               
    }
}
