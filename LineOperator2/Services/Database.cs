﻿using LineOperator2.Models;
using LineOperator2.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Xamarin.Essentials;


namespace LineOperator2.Services
{
    public class Database
    {
        static SQLiteConnection _database;

        static ObservableCollection<Product> parts;

        static public event EventHandler PartsListIsUpdated;

        static SortedDictionary<string, JobViewModel> lineJobs;


        public static List<string> GetPartNames()
        {
            var q = from part in parts orderby part.PartName select part.PartName;

            return new List<string>(q);
        }


        public static Product GetProduct(string partName)
        {
            var r = from part in parts where part.PartName == partName select part ;
            if (r.Count() > 0)
                return r.First<Product>();
            else
                return new Product() {ID = -1, PartName="", Title="" };
        }


        public static ObservableCollection<Product> GetParts()
        {
            InitializeDB();
            return parts;
        }


        public static string GetDBFileName()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ETCOperations.sl3");
            return dbPath;
        }

        static void InitializeDB()
        {
            if (_database != null)
                return;

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ETCOperations.sl3");
            _database = new SQLiteConnection(dbPath);

            //_database.DropTable<Pin>();
            //_database.DropTable<Product>();
            //_database.DropTable<Job>();

            _database.CreateTable<Pin>();
            _database.CreateTable<Product>();
            _database.CreateTable<Job>();
            //_database.DeleteAll<Job>();


            parts = new ObservableCollection<Product>(_database.Query<Product>("SELECT * FROM Product ORDER BY PartName ASC"));

            List<Job> jobs = new List<Job>(_database.Query<Job>("SELECT *, Max(ID) FROM JOB Group By Line ORDER BY Line ASC"));

            
            if (jobs.Count > 0)
            {
                lineJobs = new SortedDictionary<string, JobViewModel>();
                foreach (var job in jobs)
                {
                    //I think this will be temporary until the record is correct.
                    if(job.Part == null)
                    {
                        job.Part = new Product();
                    }
                    job.Read();
                    lineJobs.Add(job.Line, new JobViewModel(job));
                }

                if (jobs.Count <= 8)
                {
                    AddUpdateJob(new Job("Line 8"));
                    AddUpdateJob(new Job("Line 9"));
                    AddUpdateJob(new Job("Line 10"));
                }
                //let's change "Line 10"
                if (lineJobs.ContainsKey("Line 10"))
                {
                    var j = lineJobs["Line 10"];
                    j.Line = "Line A";
                    _database.Update(j.Job);
                }

            }
            else
            {

                lineJobs = new SortedDictionary<string, JobViewModel>();

                AddUpdateJob(new Job("Line 0"));
                AddUpdateJob(new Job("Line 1"));
                AddUpdateJob(new Job("Line 2"));
                AddUpdateJob(new Job("Line 3"));
                AddUpdateJob(new Job("Line 4"));
                AddUpdateJob(new Job("Line 5"));
                AddUpdateJob(new Job("Line 6"));
                AddUpdateJob(new Job("Line 7"));

                AddUpdateJob(new Job("Line 9"));
                AddUpdateJob(new Job("Line 10"));
            }

        }

        internal static void SendDBToDev()
        {
            var message = new EmailMessage
            {
                Subject = "db file from line operator app",
                To = new List<string>() { "woody.a.anderson@gmail.com" }
            };

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ETCOperations.sl3");
            message.Attachments.Add(new EmailAttachment(dbPath));
        }

        internal static Pin GetPinPoint(int jobID)
        {
            Pin result = new Pin();
            try
            {
                var qresults = _database.Query<Pin>("SELECT *, MAX(PinTime) FROM Pin GROUP BY JobID HAVING JobID=?", jobID);
                result = qresults.First();
            }
            catch(Exception e)
            { 
            }
            return result;
        }


        internal static void AddUpdateJob(Job job)
        {
            try
            {
                var found = _database.Get<Job>(job.ID);
                if (found != null)
                {
                    _database.Update(job);
                }
                else
                {
                    _database.Insert(job);
                }
            }
            catch(Exception e)
            {
                var resultCount = _database.Insert(job);
                Console.WriteLine(e);
                var found = _database.Get<Job>(job.ID);
            }

            lineJobs[job.Line] = new JobViewModel(job);
        }



        internal static void AddUpdatePin(Pin pinPoint)
        {
            InitializeDB();
            _database.Insert(pinPoint);
        }


        internal static void Insert(Job job)
        {
            InitializeDB();

            _database.Insert(job);
        }


        internal static Product GetPart(string partName)
        {
            var foundPart = parts.First(part => part.PartName == partName);
            return foundPart;
        }

        public static Product AddOrUpdate(Product newPart)
        {
            InitializeDB();
            Product result = null;

            try
            {
                var existingPart = parts.First(part => part.PartName == newPart.PartName && part.CutLength == newPart.CutLength);

                existingPart.PartName = newPart.PartName;
                existingPart.CutLength = newPart.CutLength;
                existingPart.PartsPerBox = newPart.PartsPerBox;
                existingPart.Multiplier = newPart.Multiplier;
                existingPart.BoxLength = newPart.BoxLength;
                existingPart.PalletSize = newPart.PalletSize;
                existingPart.CrateSize = newPart.CrateSize;

                int resultCount = _database.Update(existingPart);
                result = existingPart;
            }
            catch (Exception e)
            {
                _database.Insert(newPart);
                parts.Add(newPart);
                result = newPart;
            }

            PartsListIsUpdated?.Invoke(null, null);

            return result;
        }


        public static JobViewModel GetJobView(string lineID)
        {
            InitializeDB();

            JobViewModel result = null;
            try
            {
                result = lineJobs[lineID];
            }
            catch (KeyNotFoundException)
            { }

            return result;
        }


        public static List<JobViewModel> GetListOfJobViews()
        {
            InitializeDB();

            var q = from pair in lineJobs select pair.Value;
            return new List<JobViewModel>(q);
        }


        public static void SaveJobs()
        {
            InitializeDB();
            //_database.InsertAll(lineJobs);
            try
            {
                foreach (var lj in lineJobs)
                {
                    _database.Update(lj.Value);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

}
