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
using android_service_stack.ServiceBusiness.model;


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
            LogisticsBusiness business = new LogisticsBusiness();
            return business.getAreas(req.companyId);
        }

		//入库单 出库单 列表
		//入库1 出库2
		public StoragBlResponse GET(ServiceModel.message.StorageBl req)
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
        //盘点单列表 1盘点计划 2盘点历史
        public StorageCheckBlResponse GET(StorageCheckBl req)
        {
            StorageBlBusiness business = new StorageBlBusiness();
            if (req.type == 1)
            {
                return business.getStorageCheckPlanBls(req.companyId, req.staffId);
            }
            else if (req.type == 2)
            {
                return business.getStorageCheckHistoryBls(req.companyId, req.staffId);
            }
            else {
                return new StorageCheckBlResponse();
            }
        }
		//单据单详情
		//入库1 出库2 盘点4
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
		//执行入库 出库 移库 盘点
		//入库1 出库2 移库3 盘点4
		public NullResponse POST(Storage req)
        {
            StorageBusinesscs business = new StorageBusinesscs();
            if (!req.storageRequest.IsEmpty())
            {

                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(req.storageRequest)))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(StorageJsonModel));
                    StorageJsonModel model = (StorageJsonModel)serializer.ReadObject(ms);
                    StorageModel storageModel = new StorageModel();
                    StorageModel.Goods[] goodsList=new StorageModel.Goods[model.goodsList.Count()];
                    for (int i = 0; i < model.goodsList.Count(); i++)
                    {
                        StorageModel.Goods goods = new StorageModel.Goods();
                        goods.blId = model.goodsList[i].blId;
                        goods.logisticId = model.goodsList[i].logisticId;
                        goods.spill = model.goodsList[i].spill;
                        goodsList[i] = goods;
                    }
                    storageModel.goodsList = goodsList;
                    storageModel.staffId = model.staffId;
                    storageModel.areaId = model.areaId;
                    storageModel.corpId = model.corpId;
                    storageModel.companyId = model.companyId;

                    if (req.type == 1)
                    {
                        return business.storageIn(storageModel);
                    }
                    else if (req.type == 2)
                    {
                        return business.storageOut(storageModel);
                    }
                    else if (req.type == 3)
                    {
                        return business.storageTransform(storageModel);
                    }
                    else if (req.type == 4)
                    {
                        return business.storageCheck(storageModel);
                    }
                }
            }
            NullResponse res = new NullResponse();
            res.message = "无logisticId";
            return res;
        }
        //通过货物id获取货物信息
        public GoodsDetailResponse GET(GoodsDetail req)
        {
            LogisticsBusiness business = new LogisticsBusiness();
        return business.getGoodsDetailById(req.goodsId);
        }

    }

}