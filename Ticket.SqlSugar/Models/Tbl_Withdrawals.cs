using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Withdrawals")]
    public partial class Tbl_Withdrawals
    {
           public Tbl_Withdrawals(){


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
           /// Desc:提现单号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string WithdrawalNo {get;set;}

           /// <summary>
           /// Desc:流水
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Number {get;set;}

           /// <summary>
           /// Desc:提现金额
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Money {get;set;}

           /// <summary>
           /// Desc:提现状态(1: 审核中; 2: 驳回; 3: 提现中; 4：成功)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Status {get;set;}

           /// <summary>
           /// Desc:转账手续费
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public decimal TransferHandlingFee {get;set;}

           /// <summary>
           /// Desc:支付宝或微信的结算手续费
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public decimal PaymentHandlingFee {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:审核备注
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string AuditRemark {get;set;}

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
           /// Desc:审核时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? AuditTime {get;set;}

           /// <summary>
           /// Desc:审核人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? AuditUserId {get;set;}

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
