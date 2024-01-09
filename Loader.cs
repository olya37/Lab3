using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Calc_LAB2_Dolganova
{
    internal class Loader
    {

        public List<double> LoadListFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip
            };
            return JsonSerializer.Deserialize<List<double>>(json, options);
        }


        public List<double> LoadListFromXml(string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Steps));
            using (FileStream fs = new FileStream("steps.xml", FileMode.OpenOrCreate))
            {
                Steps? steps = xmlSerializer.Deserialize(fs) as Steps;
                return steps.Results;
            }

           
        }

        public List<double> LoadListFromSql()
        {
            string connectionString = "Data Source=mydatabase.db";
            Steps steps = new Steps();
            string stepsstring;

            using (var con = new SqliteConnection(connectionString))
            {
                con.Open();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Results FROM Steps WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", 1);

                    using (var re   ader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stepsstring = reader.GetString(1);
                            string[] dataArray = stepsstring.Split(',');
                            List<string> dataList = new List<string>(dataArray);
                            List<double> dataListi = dataList.Select(x => double.Parse(x)).ToList();
                            steps.Results = dataListi;
                        }
                        else
                        {
                            steps.Results = [0, 0, 0];
                        }
                    }
                }
            }



            return steps.Results;
        }

    }
}
