using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using android_service_stack.ServiceModel;
using System.Data;
using System.Data.SqlClient;
using android_service_stack.ServiceModel.message;
using android_service_stack.ServiceModel.response;
using android_service_stack.ServiceBusiness;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;
using System.IO;
using System.Text;
using android_service_stack.ServiceInterface.model;
using ServiceStack.Web;


namespace android_service_stack.ServiceInterface
{
    public class MyServices : Service
    {
        //登录
        public LoginResponse POST(Login req)
        {
            LoginBusiness loginBusniss = new LoginBusiness();
            return loginBusniss.login(req.userName, req.userPassWord);
        }
        //图片上传
        public NullResponse POST(Upload req)
        {
           IHttpFile file= Request.Files[0];

           return new UploadBusiness().upload(file, req.blItemId, req.blId);
        }
       
        //获取物流公司列表
        public LogisticsCompanyResponse GET(LogisticsCompany req)
        {
            LogisticsBusiness business = new LogisticsBusiness();
            return business.getLogisticsCompany(req.companyId);
        }

        //获取库区列表
        public StorageAreasResponse GET(StorageArea req)
        {
            StorageTransfrom business = new StorageTransfrom();
            return business.getAreas(req.companyId);
        }

        //入库单列表
        //public StoragBlResponse GET(StorageInBl req)
        //{
        //    StorageBlBusiness business = new StorageBlBusiness();
        //    return business.getStorageInBls(req.companyId, req.plan);
        //}
        //入库单详情
        public StorageBlDetailResponse GET(StorageInBlDetail req)
        {
            StorageBlDetailBusiness business = new StorageBlDetailBusiness();
            return business.getStoregeInBlDetail(req.companyId, req.corpId);
        }
        //出库单详情
        public StorageBlDetailResponse GET(StorageOutBlDetail req)
        {
            StorageBlDetailBusiness business = new StorageBlDetailBusiness();
            return business.getStoregeOutBlDetail(req.companyId, req.corpId);
        }
        //盘点单列表
        public StorageCheckBlResponse GET(StorageCheckBl req)
        {
            StorageBlBusiness business = new StorageBlBusiness();
            return business.getStorageCheckBls(req.companyId, req.staffId);
        }
        //盘点单详情
        public StorageBlDetailResponse GET(StorageCheckBlDetail req)
        {
            StorageBlDetailBusiness business = new StorageBlDetailBusiness();
            return business.getStoregeCheckDetail(req.companyId, req.blId);
        }
        //入库 出库 移库 盘点
        public NullResponse POST(Storage req)
        {
            StorageBusinesscs business = new StorageBusinesscs();
            if (!req.storageRequest.IsEmpty())
            {

                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(req.storageRequest)))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(StorageJsonModel));
                    StorageJsonModel model = (StorageJsonModel)serializer.ReadObject(ms);
                    if (req.type == 1)
                    {
                        return business.storageIn(model.blIds, model.companyId, model.logisticIds, model.spills, model.corpId, model.staffId);
                    }
                    else if(req.type == 2)
                    {
                        return business.storageOut(model.blIds, model.companyId, model.logisticIds);
                    }
                    else if (req.type == 3)
                    {
                        return business.storageTransform(model.areaId, model.companyId, model.logisticIds);
                    }
                    else if (req.type == 4)
                    {
                        return business.storageCheck(model.blIds, model.companyId, model.logisticIds);
                    }
                }
            }
             NullResponse res =new NullResponse();
             res.message = "无logisticId";
             return res;
        }
        //出库
        //public NullResponse POST(StorageOut req)
        //{
        //    StorageBusinesscs business = new StorageBusinesscs();
        //    if (!req.storageOutRequest.IsEmpty())
        //    {

        //        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(req.storageOutRequest)))
        //        {
        //            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(StorageJsonModel));
        //            StorageJsonModel model = (StorageJsonModel)serializer.ReadObject(ms);
        //            return business.storageOut(model.blIds, model.companyId, model.logisticIds);
        //        }
        //    }
        //    NullResponse res = new NullResponse();
        //    res.message = "无出库logisticId";
        //    return res;
        //}
        //盘点
        //public NullResponse POST(StorageCheck req)
        //{
        //    StorageBusinesscs business = new StorageBusinesscs();
        //    if (!req.storageCheckRequest.IsEmpty())
        //    {

        //        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(req.storageCheckRequest)))
        //        {
        //            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(StorageJsonModel));
        //            StorageJsonModel model = (StorageJsonModel)serializer.ReadObject(ms);
        //            return business.storageCheck(model.blIds, model.companyId, model.logisticIds);
        //        }
        //    }
        //    NullResponse res = new NullResponse();
        //    res.message = "无入库logisticId";
        //    return res;
        //}
        //移库
        //public NullResponse POST(StorageTransform req)
        //{
        //    StorageBusinesscs business = new StorageBusinesscs();
        //   if (!req.storageTransformRequest.IsEmpty())
        //    {

        //        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(req.storageTransformRequest)))
        //        {
        //            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(StorageJsonModel));
        //            StorageJsonModel model = (StorageJsonModel)serializer.ReadObject(ms);
        //            return business.storageTransform(model.areaId,model.companyId, model.logisticIds);
        //        }
        //    }
        //    NullResponse res = new NullResponse();
        //    res.message = "无移库logisticId";
        //    return res;
        //}
    }

}