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
            IHttpFile file = Request.Files[0];

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

        //入库单 出库单 列表
        public StoragBlResponse GET(android_service_stack.ServiceModel.message.StorageBl req)
        {
            StorageBlBusiness business = new StorageBlBusiness();
            if (req.type == 1)
            {
                return business.getStorageInBls(req.companyId);
            }
            else if (req.type == 2)
            {
                return business.getStorageOutBls(req.companyId);
            }

            return new StoragBlResponse();
        }
        //入库单详情
        public StorageBlDetailResponse GET(StorageInBlDetail req)
        {
            StorageBlDetailBusiness business = new StorageBlDetailBusiness();
            if (req.type == 1)
            {
                return business.getStoregeInBlDetail(req.companyId, req.corpId);
            }
            else if (req.type == 2)
            {
                return business.getStoregeOutBlDetail(req.companyId, req.corpId);
            }
            else if (req.type == 4)
            {
                return business.getStoregeCheckDetail(req.companyId, req.blId);
            }
            return new StorageBlDetailResponse();

        }

        //盘点单列表
        public StorageCheckBlResponse GET(StorageCheckBl req)
        {
            StorageBlBusiness business = new StorageBlBusiness();
            return business.getStorageCheckBls(req.companyId, req.staffId);
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
                    else if (req.type == 2)
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
            NullResponse res = new NullResponse();
            res.message = "无logisticId";
            return res;
        }

    }

}