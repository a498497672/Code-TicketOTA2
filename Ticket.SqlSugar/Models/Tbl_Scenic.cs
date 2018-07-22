using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Scenic")]
    public partial class Tbl_Scenic
    {
           public Tbl_Scenic(){


           }
           /// <summary>
           /// Desc:景区Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ScenicId {get;set;}

           /// <summary>
           /// Desc:企业Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int EnterpriseId {get;set;}

           /// <summary>
           /// Desc:景区名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ScenicName {get;set;}

           /// <summary>
           /// Desc:景区代码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ScenicCode {get;set;}

           /// <summary>
           /// Desc:景区级别：1：一级，2：二级，3：三级，4：四级，5：五级
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ScenicLevel {get;set;}

           /// <summary>
           /// Desc:街道地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ScenicAddress {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ProvinceId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CityId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? DistrictId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ProvinceName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string CityName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DistrictName {get;set;}

           /// <summary>
           /// Desc:完整地址(含省市区)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string FullAddress {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Tel {get;set;}

           /// <summary>
           /// Desc:景区详情
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Summary {get;set;}

           /// <summary>
           /// Desc:景区主图
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string MainImg {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ContactMobile {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int SmsCount {get;set;}

           /// <summary>
           /// Desc:排序号(降序)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OrderId {get;set;}

           /// <summary>
           /// Desc:状态(以二进制的方式存储, 第1位:是否停用，第2位：短信是否停用)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DataStatus {get;set;}

           /// <summary>
           /// Desc:
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? LastUpdateTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? LastUpdateUserId {get;set;}

           /// <summary>
           /// Desc:签名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SignName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TicketTips {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SYSCodeImg {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SYSCode {get;set;}

    }
}
