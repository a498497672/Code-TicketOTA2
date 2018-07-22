using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_DoorGate")]
    public partial class Tbl_DoorGate
    {
           public Tbl_DoorGate(){


           }
           /// <summary>
           /// Desc:景区闸机ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int DoorGateId {get;set;}

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
           /// Desc:园门Id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ScenicGateId {get;set;}

           /// <summary>
           /// Desc:闸机机身号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string DoorGateNO {get;set;}

           /// <summary>
           /// Desc:闸机名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string DoorGateName {get;set;}

           /// <summary>
           /// Desc:闸机类别 1：三辊闸机 2：翼闸
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DoorGateTypeId {get;set;}

           /// <summary>
           /// Desc:检票方式 1：一票多人
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CheckTicketWay {get;set;}

           /// <summary>
           /// Desc:闸机ReadNO
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ReadNO {get;set;}

           /// <summary>
           /// Desc:闸机DoorIDCard
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DoorIDCard {get;set;}

           /// <summary>
           /// Desc:景区备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:后台备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AdminRemark {get;set;}

           /// <summary>
           /// Desc:排序号(降序)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OrderId {get;set;}

           /// <summary>
           /// Desc:数据状态(以二进制的方式存储,第1位:是否停用)
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
           /// Desc:多次入园(0:禁用,1:出园采集,2:认证)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int SupportState {get;set;}

    }
}
