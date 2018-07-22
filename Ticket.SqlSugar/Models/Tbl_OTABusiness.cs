using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_OTABusiness")]
    public partial class Tbl_OTABusiness
    {
           public Tbl_OTABusiness(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int EnterpriseId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ScenicId {get;set;}

           /// <summary>
           /// Desc:分销商ID（2位分销商类型+年月日（年取后两位）+3位顺序号）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OTAID {get;set;}

           /// <summary>
           /// Desc:分销商身份标识
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string IdentityKey {get;set;}

           /// <summary>
           /// Desc:盐值码(用于加密算法)
           /// Default:newid()
           /// Nullable:False
           /// </summary>           
           public Guid Saltcode {get;set;}

           /// <summary>
           /// Desc:企业名称（全称）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string FullName {get;set;}

           /// <summary>
           /// Desc:分销商简称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ShortName {get;set;}

           /// <summary>
           /// Desc:商家类型(1:OTA;2:旅行社)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int BusinessType {get;set;}

           /// <summary>
           /// Desc:合同到期时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ExpireTTime {get;set;}

           /// <summary>
           /// Desc:省
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ProvinceId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ProvinceName {get;set;}

           /// <summary>
           /// Desc:市
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CityId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CityName {get;set;}

           /// <summary>
           /// Desc:区
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DistrictId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DistrictName {get;set;}

           /// <summary>
           /// Desc:详细地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Address {get;set;}

           /// <summary>
           /// Desc:联系人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ContractName {get;set;}

           /// <summary>
           /// Desc:联系电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ContractMobile {get;set;}

           /// <summary>
           /// Desc:固定电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TellPhone {get;set;}

           /// <summary>
           /// Desc:邮件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Email {get;set;}

           /// <summary>
           /// Desc:营业执照号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LicenseNo {get;set;}

           /// <summary>
           /// Desc:税务号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TaxNo {get;set;}

           /// <summary>
           /// Desc:组织机构代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OrganizationCode {get;set;}

           /// <summary>
           /// Desc:法人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Corporation {get;set;}

           /// <summary>
           /// Desc:企业性质
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? EnterpriseType {get;set;}

           /// <summary>
           /// Desc:企业工商登记时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? RegistTime {get;set;}

           /// <summary>
           /// Desc:业务经营范围
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BusinessDescription {get;set;}

           /// <summary>
           /// Desc:分销商状态（二进制位表示：第一位表示有效状态）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DataStatus {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
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
           /// Desc:分销商编码（对接小径平台的分销商编码）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Code {get;set;}

    }
}
