using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_WeChatAccount")]
    public partial class Tbl_WeChatAccount
    {
           public Tbl_WeChatAccount(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:类型， 1 代表 风景智联， 0表示景区的
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int Type {get;set;}

           /// <summary>
           /// Desc:微信应用ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string AppId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string AppSecret {get;set;}

           /// <summary>
           /// Desc:用于微信支付的PartnerKey
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string PartnerKey {get;set;}

           /// <summary>
           /// Desc:用于微信支付的商户号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string MchId {get;set;}

           /// <summary>
           /// Desc:微信异步回调域名,不需要前缀http://
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string BackDomain {get;set;}

           /// <summary>
           /// Desc:证书路径地址
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string CertPath {get;set;}

           /// <summary>
           /// Desc:退款协议路径地址
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string RefundAgreementPath {get;set;}

           /// <summary>
           /// Desc:企业id
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int EnterpriseId {get;set;}

           /// <summary>
           /// Desc:景区id
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int ScenicId {get;set;}

           /// <summary>
           /// Desc:费率
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public double Rate {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:创建人
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserId {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? LastUpdateTime {get;set;}

           /// <summary>
           /// Desc:修改人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? LastUpdateUserId {get;set;}

    }
}
