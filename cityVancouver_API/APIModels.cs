using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Design;

namespace cityVancouver_API
{
    public class FoodCartContext : DbContext
    {
        String connectionString = "Server=.;Database=cityOfVacouverApi;Trusted_Connection=True;";

        public DbSet<FoodCart> Root { get; set; }

        // this sets up connection string 
        // add migration initial create 
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(connectionString);
        }
    }

    public class FoodCart
    {
        [Key]
        public string key { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string geo_localarea { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public string location { get; set; }
        public string vendor_type { get; set; }
        public string business_name { get; set; }
    }


    public class Rootobject
    {
        public int nhits { get; set; }
        public Parameters parameters { get; set; }
        public Record[] records { get; set; }
    }

    public class Parameters
    {
        public string dataset { get; set; }
        public string timezone { get; set; }
        public int rows { get; set; }
        public string format { get; set; }
    }

    public class Record
    {
        public string datasetid { get; set; }
        public string recordid { get; set; }
        public Fields fields { get; set; }
        public DateTime record_timestamp { get; set; }
    }

    public class Fields
    {
        public string status { get; set; }
        public string description { get; set; }
        public string geo_localarea { get; set; }
        public Geom geom { get; set; }
        public string location { get; set; }
        public string key { get; set; }
        public string vendor_type { get; set; }
        public string business_name { get; set; }
    }

    public class Geom
    {
        public string type { get; set; }
        public float[] coordinates { get; set; }
    }
}
