using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Ledger")]
    public partial class Tbl_Ledger
    {
           public Tbl_Ledger(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

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
           /// Desc:子商户号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SubMerchantNumber {get;set;}

           /// <summary>
           /// Desc:流水号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Number {get;set;}

           /// <summary>
           /// Desc:交易类型， 1 门票支付， 2 门票退款 3 提现
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int Type {get;set;}

           /// <summary>
           /// Desc:交易金额
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Money {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:交易方式  1 支付宝 2 微信
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int PayType {get;set;}

           /// <summary>
           /// Desc:商户订单号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OrderNo {get;set;}

           /// <summary>
           /// Desc:商品名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string CommodityName {get;set;}

           /// <summary>
           /// Desc:商品金额
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal CommodityMoney {get;set;}

           /// <summary>
           /// Desc:提现审核表id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? WithdrawalsId {get;set;}

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
