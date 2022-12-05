using Ganss.Excel;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadBiomentrics.Model;

namespace UploadBiomentrics
{
    class Program
    {
        static void Main(string[] args)
        {

            var biometricdatas = new ExcelMapper(@"D:\Documents\data1.xlsx").Fetch<BiometricsData>().ToList(); //get excel path
            InsertManyBiometrics(biometricdatas);
            Console.ReadKey();

        }

        const string MongoDBConnectionString = "mongodb://localhost:27017/";
        private static void InsertManyBiometrics(ICollection<BiometricsData> documents)
        {
            var client = new MongoClient(MongoDBConnectionString);
            var database = client.GetDatabase("payrolldb");
            var collection = database.GetCollection<BiometricsData>("biometricsData");


            try
            {
                foreach (var item in documents)
                {
                    var a = collection.Find(x => x.ElectronicId == item.ElectronicId && x.LogDate == item.LogDate).FirstOrDefault();
                    if (a == null)
                    {
                        collection.InsertOne(item);
                    }
                }
                Console.WriteLine("Finished uploading biometrics collection");

            }
            catch (Exception e)
            {
                collection.DeleteMany(c => c.CreateDate == DateTime.UtcNow.Date);
                Console.WriteLine("Error writing to MongoDB: " + e.Message);
            }

        }
    }
}




