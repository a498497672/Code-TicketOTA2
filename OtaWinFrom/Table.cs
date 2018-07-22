using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OtaWinFrom
{
    public class Table
    {
        public static DataTable ListToTable<T>(List<T> list)
        {
            var dt = new DataTable();
            var type = typeof(T);
            var tableName = type.Name;
            var properties = type.GetProperties();
            foreach (PropertyInfo item in properties)
            {
                dt.Columns.Add(item.Name);
            }
            foreach (var entity in list)
            {
                var row = dt.NewRow();
                foreach (PropertyInfo item in properties)
                {
                    if (item.CanRead)
                    {
                        row[item.Name] = item.GetValue(entity, null);
                    }
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static DataTable ObjectToTable<T>(T entity)
        {
            var dt = new DataTable();
            var type = typeof(T);
            var tableName = type.Name;
            var properties = type.GetProperties();
            foreach (PropertyInfo item in properties)
            {
                dt.Columns.Add(item.Name);
            }
            var row = dt.NewRow();
            foreach (PropertyInfo item in properties)
            {
                if (item.CanRead)
                {
                    row[item.Name] = item.GetValue(entity, null);
                }
            }
            dt.Rows.Add(row);
            return dt;
        }
    }
}
