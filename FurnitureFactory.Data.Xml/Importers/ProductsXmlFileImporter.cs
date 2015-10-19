using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace FurnitureFactory.Data.Xml.Importers
{
    public class ProductsXmlFileImporter: XmlFileImporter
    {
        public override void ImportXmlData(string filepath)
        {
            string xml = File.ReadAllText(filepath);

            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection conection = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("InsertXML"))
                {
                    cmd.Connection = conection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@xml", xml);
                    conection.Open();
                    cmd.ExecuteNonQuery();
                    conection.Close();
                }
            }
        }
    }
}
