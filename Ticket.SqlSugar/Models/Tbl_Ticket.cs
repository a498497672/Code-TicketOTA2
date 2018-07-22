using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Ticket")]
    public partial class Tbl_Ticket
    {
           public Tbl_Ticket(){


           }
           /// <summary>
           /// Desc:门票ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int TicketId {get;set;}

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
           /// Desc:门票名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TicketName {get;set;}

           /// <summary>
           /// Desc:售卖有效期开始
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime ExpiryDateStart {get;set;}

           /// <summary>
           /// Desc:售卖有效期结束
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime ExpiryDateEnd {get;set;}

           /// <summary>
           /// Desc:市场价格
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? MarkPrice {get;set;}

           /// <summary>
           /// Desc:销售价格
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal SalePrice {get;set;}

           /// <summary>
           /// Desc:日库存量(0:表示不限制)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? StockCount {get;set;}

           /// <summary>
           /// Desc:当日已售数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? SellCount {get;set;}

           /// <summary>
           /// Desc:1：默认全部通过，2：全不通过，3：指定闸机（此时和闸机关联表联合）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CheckWay {get;set;}

           /// <summary>
           /// Desc:延时验证(默认：0。即可马上验证) 分钟
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? DelayCheck {get;set;}

           /// <summary>
           /// Desc:排序号(降序)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OrderId {get;set;}

           /// <summary>
           /// Desc:数据状态(以二进制的方式存储,第1位:是否下架)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DataStatus {get;set;}

           /// <summary>
           /// Desc:
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
           /// Desc:1：景区自己的，2：OTA
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketSource {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int TypeId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SettlementPrice {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public bool CanRefund {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public bool CanModify {get;set;}

           /// <summary>
           /// Desc:门票规则Id(关联Tbl_TicketRule表主键Id)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int RuleId {get;set;}

           /// <summary>
           /// Desc:年票挂失费
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public decimal? LossFee {get;set;}

           /// <summary>
           /// Desc:门票产品编码（对接小径平台的产品编码）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Code {get;set;}

           /// <summary>
           /// Desc:最小起订量默认0
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int MinOQ {get;set;}

           /// <summary>
           /// Desc:最大起订量默认10000
           /// Default:10000
           /// Nullable:False
           /// </summary>           
           public int MaxOQ {get;set;}

           /// <summary>
           /// Desc:产品上架渠道设置 以，隔开  1 散客票购票 2 团队票购票 3 手机扫码购票
           /// Default:1,2,3
           /// Nullable:False
           /// </summary>           
           public string ShelvesChannel {get;set;}

    }
}
