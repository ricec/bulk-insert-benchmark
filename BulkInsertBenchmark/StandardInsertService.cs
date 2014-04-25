using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BulkInsertBenchmark
{
    public static class StandardInsertService
    {
        public static void Insert(List<Item> items)
        {
            string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=StandardInsertTest;Integrated Security=SSPI;";

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("INSERT INTO Items(Name, Depth, Height, Width) VALUES(@name, @depth, @height, @width)", conn))
                {
                    // Add params
                    var nameParam = cmd.Parameters.Add("@name", SqlDbType.VarChar, 50);
                    var depthParam = cmd.Parameters.Add("@depth", SqlDbType.Int);
                    var heightParam = cmd.Parameters.Add("@height", SqlDbType.Int);
                    var widthParam = cmd.Parameters.Add("@width", SqlDbType.Int);

                    conn.Open();

                    // Execute command for each item
                    foreach (var item in items)
                    {
                        nameParam.Value = item.Name;
                        depthParam.Value = item.Depth;
                        heightParam.Value = item.Height;
                        widthParam.Value = item.Width;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
