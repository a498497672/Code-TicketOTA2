using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_BankAccount")]
    public partial class Tbl_BankAccount
    {
           public Tbl_BankAccount(){


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
           /// Desc:开户名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string AccountName {get;set;}

           /// <summary>
           /// Desc:开户支行
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OpeningBranch {get;set;}

           /// <summary>
           /// Desc:帐号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Account {get;set;}

           /// <summary>
           /// Desc:银行账户类型 1对公账户
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int BankAccountType {get;set;}

           /// <summary>
           /// Desc:账户类型 1清分 2 自结
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int AccountType {get;set;}

           /// <summary>
           /// Desc:商户号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string MerchantNumber {get;set;}

           /// <summary>
           /// Desc:预留手机号码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string phone {get;set;}

           /// <summary>
           /// Desc:子商户号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SubMerchantNumber {get;set;}

           /// <summary>
           /// Desc:交易密码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TradePwd {get;set;}

           /// <summary>
           /// Desc:可用金额
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public decimal ValidMoney {get;set;}

           /// <summary>
           /// Desc:冻结金额
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public decimal FrozenMoney {get;set;}

           /// <summary>
           /// Desc:是否设置 
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public bool Status {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
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
