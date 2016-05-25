using MongoDB.Driver;
using RealEstate.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstate.Rentals;

namespace RealEstate.App_Start.DataContext
{
    public class RealEstateContext
    {
        public MongoDatabase DataBase;
        string conStatus { get; set; }
        public RealEstateContext()
        {
            //Connection to MongoDB 
            try
            {
                var Client = new
                    MongoClient(Settings.Default.RealEstateConnectionString);
                var Server = Client.GetServer();
                DataBase = Server.GetDatabase
                    (Settings.Default.RealEstateDataBaseName);
            }
            catch (Exception e)
            {
                conStatus = e.ToString();
            }
        }

        public MongoCollection<Rental> Rentals
        {
            get { return DataBase.GetCollection<Rental>("rentals"); }
        }
    }
}