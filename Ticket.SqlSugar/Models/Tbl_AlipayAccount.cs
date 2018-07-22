using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_AlipayAccount")]
    public partial class Tbl_AlipayAccount
    {
           public Tbl_AlipayAccount(){


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
           /// Desc:alipay_public_key
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string PublicKey {get;set;}

           /// <summary>
           /// Desc:支付宝开发者私钥
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string MerchantPrivateKey {get;set;}

           /// <summary>
           /// Desc:支付宝开发者公钥
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string MerchantPublicKey {get;set;}

           /// <summary>
           /// Desc:支付宝应用ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string AppId {get;set;}

           /// <summary>
           /// Desc:支付宝合作伙伴ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Pid {get;set;}

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
