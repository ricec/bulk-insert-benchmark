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
    public static class XmlInsertService
    {
        public static void Insert(List<Item> items)
        {
            string xml;
            var serializer = new XmlSerializer(typeof(List<Item>));

            // Serialize the list to XML.
            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter))
                {
                    serializer.Serialize(xmlWriter, items);
                    xml = textWriter.ToString();
                }
            }

            // Execute the stored procedure.
            string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=XmlInsertTest;Integrated Security=SSPI;";
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("AddItems", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@xml", System.Data.SqlDbType.Xml).Value = xml;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
