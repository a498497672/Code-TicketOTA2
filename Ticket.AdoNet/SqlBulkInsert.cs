using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Ticket.Utility.Helpers
{
    public class SqlBulkInsert
    {
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="tableName">目标表</param>
        public static void Insert(DataTable dt, string tableName, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BatchSize = dt.Rows.Count;
                conn.Open();
                if (dt != null && dt.Rows.Count != 0)
                {
                    bulkCopy.WriteToServer(dt);
                }
            }
        }

        /// <summary>
        /// 批量插入 使用时T的类型名称和实体表的名称必须一致
        /// </summary>
        /// <param name="list"></param>
        /// <param name="connection"></param>
        /// <param name="trans"></param>
        public static void Inert<T>(List<T> list, SqlConnection connection, SqlTransaction trans) where T : class
        {
            var dt = new DataTable();
            var type = typeof(T);
            var tableName = type.Name;
            var properties = type.GetProperties();
            foreach (PropertyInfo item in properties)
            {
                if (item.PropertyType == typeof(System.Guid))
                {
                    dt.Columns.Add(item.Name, typeof(System.Data.SqlTypes.SqlGuid));
                }
                else
                {
                    dt.Columns.Add(item.Name);
                }
            }
            foreach (var entity in list)
            {
                var row = dt.NewRow();
                foreach (PropertyInfo item in properties)
                {
                    if (item.CanRead)
                    {
                        var val = item.GetValue(entity, null);
                        if (val != null && (item.PropertyType == typeof(DateTime) || item.PropertyType == typeof(Nullable<System.DateTime>)))
                        {
                            row[item.Name] = ((DateTime)val).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            row[item.Name] = val;
                        }
                    }
                }
                dt.Rows.Add(row);
            }
            using (SqlBulkCopy copy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, trans))
            {
                copy.DestinationTableName = tableName;
                copy.BatchSize = list.Count;
                copy.WriteToServer(dt);
            }
        }

        public static void Inert<T>(T entity, SqlConnection connection, SqlTransaction trans) where T : class
        {
            var dt = new DataTable();
            var type = typeof(T);
            var tableName = type.Name;
            var properties = type.GetProperties();
            foreach (PropertyInfo item in properties)
            {
                if (item.PropertyType == typeof(System.Guid))
                {
                    dt.Columns.Add(item.Name, typeof(System.Data.SqlTypes.SqlGuid));
                }
                else
                {
                    dt.Columns.Add(item.Name);
                }
            }
            var row = dt.NewRow();
            foreach (PropertyInfo item in properties)
            {
                if (item.CanRead)
                {
                    var val = item.GetValue(entity, null);
                    if (val != null && (item.PropertyType == typeof(DateTime) || item.PropertyType == typeof(Nullable<System.DateTime>)))
                    {
                        row[item.Name] = ((DateTime)val).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        row[item.Name] = val;
                    }
                }
            }
            dt.Rows.Add(row);
            using (SqlBulkCopy copy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, trans))
            {
                copy.DestinationTableName = tableName;
                copy.BatchSize = 1;
                copy.WriteToServer(dt);
            }
        }
    }
}
