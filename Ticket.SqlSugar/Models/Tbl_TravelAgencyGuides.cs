using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_TravelAgencyGuides")]
    public partial class Tbl_TravelAgencyGuides
    {
           public Tbl_TravelAgencyGuides(){


           }
           /// <summary>
           /// Desc:导游列表Id
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
           /// Desc:渠道商(旅行社)Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OTABusinessId {get;set;}

           /// <summary>
           /// Desc:导游名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:手机号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Mobile {get;set;}

           /// <summary>
           /// Desc:身份证
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string IdCard {get;set;}

           /// <summary>
           /// Desc:导游类型 1 导游 2司机
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int GuideType {get;set;}

           /// <summary>
           /// Desc:下单时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

    }
}
