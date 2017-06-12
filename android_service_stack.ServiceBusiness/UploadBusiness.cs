using android_service_stack.ServiceModel.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Web;
using System.IO;
using System.Web;
using System.Data.Common;
using android_service_stack.ServiceBusiness.businessbase;

namespace android_service_stack.ServiceBusiness
{
    public class UploadBusiness : BaseBusiness
    {
        public NullResponse upload(IHttpFile file, int blItemId, int blId)
        {
            String imgBasePath = "/uploadfile/images";
            String filePath = HttpContext.Current.Server.MapPath(imgBasePath);
            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
            String fileName = GetTimeStamp() + ".png";
            using (StreamWriter myStreamWriter = new StreamWriter(filePath + "/"+fileName))
            {
                byte[] buffer = StreamToBytes(file.InputStream);
                myStreamWriter.BaseStream.Write(buffer, 0, buffer.Length);
                String imgPath = imgBasePath + "/" + fileName;
                String sql = String.Format("insert TStorageBLItemFile (BLItemID,BLID,FilePath) values ({0},{1},'{2}')", blItemId, blId, imgPath);
                DbCommand cmd = getDbHelper().GetSqlStringCommond(sql);
                getDbHelper().ExecuteDataTable(cmd);
            }
            return new NullResponse();
        }
        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        } 
    }
}
