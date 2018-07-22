using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Enterprise")]
    public partial class Tbl_Enterprise
    {
           public Tbl_Enterprise(){


           }
           /// <summary>
           /// Desc:企业Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int EnterpriseId {get;set;}

           /// <summary>
           /// Desc:企业类型：1：景区，2：旅行社，3：分销商
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int EnterpriseTypeId {get;set;}

           /// <summary>
           /// Desc:企业名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string EnterpriseName {get;set;}

           /// <summary>
           /// Desc:联系人
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ContactPerson {get;set;}

           /// <summary>
           /// Desc:联系人手机
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ContactMobile {get;set;}

           /// <summary>
           /// Desc:街道地址
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ContactAddress {get;set;}

           /// <summary>
           /// Desc:省Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ProvinceId {get;set;}

           /// <summary>
           /// Desc:市Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CityId {get;set;}

           /// <summary>
           /// Desc:区Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? DistrictId {get;set;}

           /// <summary>
           /// Desc:省
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ProvinceName {get;set;}

           /// <summary>
           /// Desc:市
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string CityName {get;set;}

           /// <summary>
           /// Desc:区
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DistrictName {get;set;}

           /// <summary>
           /// Desc:完整地址(含省市区街道)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string FullAddress {get;set;}

           /// <summary>
           /// Desc:固定电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Tel {get;set;}

           /// <summary>
           /// Desc:电子邮件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Email {get;set;}

           /// <summary>
           /// Desc:营业执照
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string BusinessLicense {get;set;}

           /// <summary>
           /// Desc:营业执照图片
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BusinessLicenseImg {get;set;}

           /// <summary>
           /// Desc:税务登记证
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RegistrationCertificate {get;set;}

           /// <summary>
           /// Desc:税务登记证图片
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RegistrationCertificateImg {get;set;}

           /// <summary>
           /// Desc:组织机构代码证
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OrganizationCode {get;set;}

           /// <summary>
           /// Desc:组织机构代码证图片
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OrganizationCodeImg {get;set;}

           /// <summary>
           /// Desc:法人代表
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LegalRepresentative {get;set;}

           /// <summary>
           /// Desc:企业性质 1：有限责任公司。2：股份有限公司。。。。
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int EnterpriseNature {get;set;}

           /// <summary>
           /// Desc:网址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SiteUrl {get;set;}

           /// <summary>
           /// Desc:传真
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Fax {get;set;}

           /// <summary>
           /// Desc:QQ
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QQ {get;set;}

           /// <summary>
           /// Desc:注册时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? RegistrationDate {get;set;}

           /// <summary>
           /// Desc:经营范围
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string BusinessScope {get;set;}

           /// <summary>
           /// Desc:可用短信数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? SmsCount {get;set;}

           /// <summary>
           /// Desc:排序号(降序)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OrderId {get;set;}

           /// <summary>
           /// Desc:状态(以二进制的方式存储,第一位：是否启用；第2位:是否已审核;第3位:是否审核通过.4: 短信是否启用)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DataStatus {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:创建人Id
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int CreateUserId {get;set;}

           /// <summary>
           /// Desc:上次修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? LastUpdateTime {get;set;}

           /// <summary>
           /// Desc:上次修改人ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? LastUpdateUserId {get;set;}

    }
}
