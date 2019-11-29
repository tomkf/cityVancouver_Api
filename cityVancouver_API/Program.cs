using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace cityVancouver_API
{
    class Program
    {
        static void Main(string[] args)
        {
            const string protocol = "https";
            const string baseURL = "://opendata.vancouver.ca/api/records/1.0/search/?dataset=food-vendors&rows=1000";

            Uri uri = new Uri(protocol + baseURL);

            string jsonString = CallRestMethod(uri);

            Rootobject rootObject = JsonConvert.DeserializeObject<Rootobject>(jsonString);

            FoodCartContext db = new FoodCartContext();

            foreach (Record cart in rootObject.records)
            {
                db.Add(new FoodCart {
                 key = cart.fields.key,
                status = cart.fields.status,
                description = cart.fields.description,
                geo_localarea = cart.fields.geo_localarea,
                longitude = cart.fields.geom.coordinates[0],
                latitude = cart.fields.geom.coordinates[1],
                location = cart.fields.location,
                vendor_type = cart.fields.vendor_type,
                business_name = cart.fields.business_name
                });
            }
        }

        static string CallRestMethod(Uri uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            // Get the web response from the api
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Get a stream to read the reponse
            StreamReader responseStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            // Read the response and write it to the console
            // Close the connection to the api and the stream reader
            string stringResponce = responseStream.ReadToEnd();
            response.Close();
            responseStream.Close();
            return stringResponce;
        }


        static class SqlHelper
        {
            public static SqlDataReader ExecuteReader(String connString, String commandText)
            {
                SqlConnection conn = new SqlConnection(connString);
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return reader;
                }
            }
        }
    }
}