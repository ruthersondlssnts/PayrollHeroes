using Ganss.Excel;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadEmployees.Model;

namespace UploadEmployees
{
    class Program
    {
        static void Main(string[] args)
        {
            var excelObjects = new ExcelMapper(@"D:\Documents\data.xlsx").Fetch<ExcelObject>().ToList();
            excelObjects = excelObjects.GroupBy(x => x.BioId).Select(x => x.First()).ToList();
            List<Employee> employees = new List<Employee>();
            List<RefShift> refShifts = new List<RefShift>();
            int count = 1;
            foreach (var item in excelObjects)
            {
                Employee employee = new Employee();
                RefShift refShift = new RefShift();
                refShift.ShiftName = item.Timetable;
                refShift.ShiftIn = item.OnDuty;
                refShift.ShiftOut = item.OffDuty;
                refShifts.Add(refShift);


                employee.FirstName = item.Name;
                employee.LastName = item.Name;
                employee.ElectronicId = item.BioId;
                employee.RefShift.ShiftName = item.Timetable;
                employee.RefShift.ShiftIn = item.OnDuty;
                employee.RefShift.ShiftOut = item.OffDuty;
                if (item.Timetable == "SHIFT C Shift")
                {
                    employee.RefShift.IsNightDiff = true;
                    employee.RefShift.NDStart = "22:00";
                    employee.RefShift.NDEnd = "06:00";
                }
                else
                {
                    employee.RefShift.IsNightDiff = false;
                }
                employee.Password = "gyH98uU5Hepwkp27MIX1oQ==";
                employee.Username = "emp" + count;
                employee.Roles = "Employee";
                count++;
                employees.Add(employee);
            }


            refShifts = refShifts.GroupBy(x => x.ShiftName).Select(x => x.First()).ToList();
            InsertManyEmployee(employees);
            InsertManyShift(refShifts);
            Console.WriteLine("done");
        }

        const string MongoDBConnectionString = "mongodb://admin:p%40%24%24SSw0rdqwerty143@198.38.93.2:27017/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false";
        private static void InsertManyEmployee(ICollection<Employee> documents)
        {
            var client = new MongoClient(MongoDBConnectionString);
            var database = client.GetDatabase("payrolldb");
            var collection = database.GetCollection<Employee>("employee");
            try
            {
                collection.InsertMany(documents);
                Console.WriteLine("Finished uploading employee collection");

            }
            catch (Exception e)
            {
                collection.DeleteMany(c => c.CreateDate == DateTime.UtcNow.Date);
                Console.WriteLine("Error writing to MongoDB: " + e.Message);
            }

        }
        private static void InsertManyShift(ICollection<RefShift> documents)
        {
            var client = new MongoClient(MongoDBConnectionString);
            var database = client.GetDatabase("payrolldb");
            var collection = database.GetCollection<RefShift>("refShift");

            try
            {
                collection.InsertMany(documents);
                Console.WriteLine("Finished uploading shift collection");

            }
            catch (Exception e)
            {
                collection.DeleteMany(c => c.CreateDate == DateTime.UtcNow.Date);
                Console.WriteLine("Error writing to MongoDB: " + e.Message);
            }

        }
    }
    public class ExcelObject
    {
        [Column("AC-No.")]
        public string BioId { get; set; }
        public string Name { get; set; }
        private string timetable;

        public string Timetable
        {
            get { return timetable; }   // get method
            set { timetable = "SHIFT " + value; }  // set method
        }
        [Column("On duty")]
        public string OnDuty { get; set; }
        [Column("Off duty")]
        public string OffDuty { get; set; }


    }
}
