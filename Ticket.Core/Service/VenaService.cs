using DocomSDK.API.Vena;
using System;
using System.Linq;
using Ticket.Utility.Extensions;

namespace Ticket.Core.Service
{
    /// <summary>
    /// 掌静脉
    /// </summary>
    public class VenaService
    {
        private string _apiUrl = "http://192.168.1.70:20000/api/VenaOCR/";
        private string _token = "6B89EC1B-C751-452E-8F95-5C08AF432636";

        /// <summary>
        /// 掌静脉特征搜索匹配调用接口
        /// </summary>
        /// <returns></returns>
        public VenaOCRAPI GetOCRAPI()
        {
            var api = new VenaOCRAPI(_apiUrl, _token);
            return api;
        }

        /// <summary>
        /// 掌静脉特征存储接口
        /// </summary>
        /// <returns></returns>
        public VenaStoreAPI GetStoreAPI()
        {
            var api = new VenaStoreAPI(_apiUrl, _token);
            return api;
        }

        //创建掌静脉特征组
        public int GetVenaGroupId()
        {
            int groupId = 0;
            var venaGroup = GetStoreAPI().GetAllVenaGroup(new PageInfo { PageIndex = 1, PageSize = 1000 });
            if (venaGroup.State)
            {
                var group = venaGroup.Data.Data.FirstOrDefault(a => a.GroupName == "风景智联特征组");
                if (group == null)
                {
                    var r = GetStoreAPI().CreateVenaGroup(new VenaGroupInfo
                    {
                        CreateDate = DateTime.Now,
                        GroupName = "风景智联特征组",
                        Remarks = "风景智联测试"
                    });
                    if (r.State)
                    {
                        groupId = r.Data.GroupID;
                    }
                }
                groupId = group.GroupID;
            }
            return groupId;
        }

        /// <summary>
        /// 创建特征
        /// </summary>
        /// <param name="feature">特征串（BASE64）</param>
        public bool CreateFeature(string feature)
        {
            int groupId = GetVenaGroupId();
            if (groupId == 0)
            {
                return false;
            }
            var obj = new VenaInfo()
            {
                Address = "",
                Birthday = DateTime.Now,
                Company = "",
                Department = "",
                EMail = "",
                FeatureInfo = new VenaFeature()
                {
                    Data = feature
                },
                GroupID = groupId,
                IdentityNumber = "",
                Number = "",
                PlaceOfBirth = "",
                Remarks = "",
                Sex = SexType.Man,
                Telphone = "",
                UserName = "",
                VenaID = 0
            };
            var r = GetStoreAPI().AddVenaFeatureInfo(obj);
            return r.State;
        }

        /// <summary>
        /// 根据掌静脉特征搜索相关人员信息
        /// </summary>
        /// <param name="feature">特征串（BASE64）</param>
        /// <returns></returns>
        public bool SearchFeature(string feature)
        {
            var r = GetOCRAPI().SearchByVena(new VenaSearchObject()
            {
                FeatureInfo = new VenaFeature()
                {
                    Data = feature
                },
                MaxCount = 1,
                MinValue = 36
            });
            if (!r.State || r.Data == null || r.Data.Count < 1)
            {
                // MessageBox.Show("搜索不到匹配的数据！" + r.Message);
                return false;
            }
            else
            {
                var info = r.Data.FirstOrDefault().Info;
                var result = GetStoreAPI().DeleteVenaFeatureInfo(info);
                if (result.State)
                {

                }
                //MessageBox.Show(string.Format("搜索到目标：{0} 得分：{1}",
                //r.Data[0].Info.UserName,
                //r.Data[0].Value.ToString()));
                return true;
            }
        }

        public string getSexy(SexType sex)
        {
            switch (sex)
            {
                case SexType.Man:
                    return "男";
                case SexType.Woman:
                    return "女";
                default:
                    return "其它";
            }
        }
    }
}
