using LineOperator2.Models;
using LineOperator2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite;

namespace LineOperator2.ViewModels
{
    public class Job
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int ID { get; set; }
        public string PartID { get; set; }
        public string Line { get; set; }
        public int TotalBoxes { get; set; }
        public string Material { get; set; }
        public int BoxesPerCrate { get; set; }


        [SQLite.Ignore]
        public ObservableCollection<Product> Parts { get; set; }


        private Pin pinpoint;
        [SQLite.Ignore]
        public Pin PinPoint
        {
            get { return pinpoint; }
            set
            {
                pinpoint = value;
                Database.AddUpdatePin(value);
            }
        }

        [SQLite.Ignore]
        public Product Part { get; set; }


        public Job()
        {
            Initialize();
        }


        public Job(string lineID)
        {
            Initialize(lineID);
        }



        private void Initialize(string lineID = "Empty")
        {
            Line = lineID;

            PinPoint = new Pin();
            Part = new Product();
            Parts = Database.GetParts();
        }



        public bool Write()
        {

            //write "this" to the database
            if (this.Part != null)
            {
                this.PartID = this.Part.PartName;
            }

            Database.AddUpdateJob(this);

            if (this.PinPoint != null)
            {
                this.PinPoint.JobID = this.ID;
                Database.AddUpdatePin(this.PinPoint);
            }

            //Write "pin" to the database with the key of "this" as a foriegn key
            //Write "product" to the database with the key of "this" as a foreign key

            return false;
        }

        public bool Read()
        {
            //Get the Part from the Part ID and put it in.
            this.Part = Database.GetProduct(this.PartID);


            //Get the PinPoint and assign it as well.
            this.PinPoint = Database.GetPinPoint(ID);
            return false;
        }
    }
}
