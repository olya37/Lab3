using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;

namespace Calc_LAB2_Dolganova
{
    internal class Saver
    {

        public void SaveListToJson(List<double> list, string filePath)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(list.GetType());
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                serializer.WriteObject(fileStream, list);
            }
        }

        public void SaveListToXml(Steps steps)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Steps));

            using (FileStream fs = new FileStream("steps.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, steps);

                
            }
        }

        public void SaveListToSql(Steps steps)
        {
            string connectionString = "Data Source=mydatabase.db;";
            using (var con = new SqliteConnection(connectionString))
            {
                con.Open();
                using (var createTable = con.CreateCommand())
                {
                    createTable.CommandText = "DELETE FROM Steps";
                    createTable.CommandText = "CREATE TABLE IF NOT EXISTS Steps (Id INTEGER, Results TEXT)";
                    createTable.ExecuteNonQuery();
                }


                using (var cmd = con.CreateCommand())
                {

                    cmd.CommandText = "INSERT INTO Steps (Id, Results) VALUES (@Id, @Results)";
                    cmd.Parameters.AddWithValue("Id", 1);
                    cmd.Parameters.AddWithValue("@Results", steps.Results);
                }
            }
        }

    }
}
