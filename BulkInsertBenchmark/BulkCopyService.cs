using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsertBenchmark
{
    public static class BulkCopyService
    {
        public static void Insert(List<Item> items)
        {
            string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=BulkCopyTest;Integrated Security=SSPI;";

            var dt = new DataTable();
            
            var col = new DataColumn();
            col.DataType = typeof(int);
            col.ColumnName = "Id";
            col.AutoIncrement = true;
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "Name";
            dt.Columns.Add(col);

            col = new DataColumn();
            col.DataType = typeof(int);
            col.ColumnName = "Depth";
            dt.Columns.Add(col);

            col = new DataColumn();
            col.DataType = typeof(int);
            col.ColumnName = "Height";
            dt.Columns.Add(col);

            col = new DataColumn();
            col.DataType = typeof(int);
            col.ColumnName = "Width";
            dt.Columns.Add(col);

            foreach (var item in items)
            {
                var row = dt.NewRow();
                row["Name"] = item.Name;
                row["Depth"] = item.Depth;
                row["Height"] = item.Height;
                row["Width"] = item.Width;

                dt.Rows.Add(row);
            }

            using (var bulkCopy = new SqlBulkCopy(connectionString))
            {
                bulkCopy.DestinationTableName = "dbo.Items";
                bulkCopy.WriteToServer(dt);
            }
        }
    }
}
