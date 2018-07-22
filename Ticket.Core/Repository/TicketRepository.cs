using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Ticket.Model.Model.TravelAgency;
using Ticket.Model.Result;
using Ticket.SqlSugar;
using Ticket.SqlSugar.Models;
using Ticket.SqlSugar.Repository;
using Ticket.Utility.Extensions;

namespace Ticket.Core.Repository
{
    public class TicketRepository : RepositoryBase<Tbl_Ticket>
    {
        /// <summary>
        /// 更新日售票数
        /// </summary>
        /// <param name="tickets"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        public void UpdateTicket(List<Tbl_Ticket> tickets, SqlConnection connection, SqlTransaction transaction)
        {
            var sql = UpdateTicketSql(tickets);
            SqlCommand updateCmd = new SqlCommand(sql, connection, transaction);
            updateCmd.ExecuteNonQuery();
        }

        private string UpdateTicketSql(List<Tbl_Ticket> tickets)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE Tbl_Ticket SET SellCount = CASE TicketId");
            foreach (var row in tickets)
            {
                sb.AppendFormat(" WHEN {0} THEN {1}", row.TicketId, row.SellCount);
            }
            sb.AppendFormat(" END where TicketId in({0})", string.Join(",", tickets.Select(a => a.TicketId.ToString()).ToList()));
            return sb.ToString();
        }

        public TPageResult<TicketViewModel> GetPageList(TicketQueryModel model)
        {
            var result = new TPageResult<TicketViewModel>();
            if (model.PlayDate.Date < DateTime.Now.Date)
            {
                return result.FailureResult(null, "游玩日期不能小于今天");
            }
            //过滤掉已过期的产品
            DateTime nowTime = model.PlayDate.Date;
            var tbl_Ticket = PredicateBuilder.True<Tbl_Ticket>();
            if (!string.IsNullOrEmpty(model.TicketName))
            {
                using (var db = DbFactory.GetSqlSugarClient())
                {
                    var total = 0;
                    var data = db.Queryable<Tbl_Ticket, Tbl_OTATicketRelation>((a, b) =>
                    new object[] { JoinType.Left, a.TicketId == b.TicketId }).
                    Where((a, b) =>
                    a.TicketName.Contains(model.TicketName) &&
                    b.OTABusinessId == model.OTABusinessId &&
                    a.TicketSource == 2 &&
                    a.ExpiryDateStart <= nowTime &&
                    a.ExpiryDateEnd >= nowTime).
                    OrderBy((a) => a.CreateTime).
                    Select((a) => new TicketViewModel
                    {
                        TicketId = a.TicketId,
                        TicketName = a.TicketName,
                        Price = a.SalePrice,
                        BookCount = 1,
                        TotalAmount = a.SalePrice,
                        Min = a.MinOQ,
                        Max = a.MaxOQ
                    }).ToPageList(model.Page, model.Limit, ref total);
                    foreach (var row in data)
                    {
                        row.BookCount = row.Min > 0 ? row.Min : 1;
                        row.TotalAmount = row.BookCount * row.Price;
                    }
                    return result.SuccessResult(data, total);
                }
            }
            using (var db = DbFactory.GetSqlSugarClient())
            {
                var total = 0;
                var data = db.Queryable<Tbl_Ticket, Tbl_OTATicketRelation>((a, b) =>
                new object[] { JoinType.Left, a.TicketId == b.TicketId }).
                Where((a, b) =>
                b.OTABusinessId == model.OTABusinessId &&
                a.TicketSource == 2 &&
                a.ExpiryDateStart <= nowTime &&
                a.ExpiryDateEnd >= nowTime).
                OrderBy((a) => a.CreateTime).
                Select((a) => new TicketViewModel
                {
                    TicketId = a.TicketId,
                    TicketName = a.TicketName,
                    Price = a.SalePrice,
                    BookCount = 1,
                    TotalAmount = a.SalePrice,
                    Min = a.MinOQ,
                    Max = a.MaxOQ
                }).ToPageList(model.Page, model.Limit, ref total);
                foreach (var row in data)
                {
                    row.BookCount = row.Min > 0 ? row.Min : 1;
                }
                return result.SuccessResult(data, total);
            }
        }
    }
}
