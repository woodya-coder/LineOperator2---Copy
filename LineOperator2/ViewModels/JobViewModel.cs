using LineOperator2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace LineOperator2.ViewModels
{
    public class JobViewModel : BaseViewModel
    {
        private Job job;

        public JobViewModel(Job source)
        {
            job = source;
            CalculatedValues = new CalculatedMetrics(job);
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), OnRefreshTimer);
        }

        private bool OnRefreshTimer()
        {
            this.CalculatedValues = new CalculatedMetrics(this.job);
            return true;
        }

        public Job Job
        {
            get { return job; }

            set
            {
                job = value;
              //  NotifyPropertyChange("");
                NotifyPropertyChange("Line");
                NotifyPropertyChange("TotalBoxes");
                NotifyPropertyChange("Material");
                NotifyPropertyChange("Parts");
                NotifyPropertyChange("MinutesPerBox");
                NotifyPropertyChange("MinutesPerPallet");
                NotifyPropertyChange("PinPoint");
                NotifyPropertyChange("Part");
                NotifyPropertyChange("BoxesPerCrate");

                NotifyPropertyChange("CurrentBox");
                NotifyPropertyChange("ShiftBoxNeeds");
                NotifyPropertyChange("ShiftPalletNeeds");
                NotifyPropertyChange("ShiftCrateNeeds");
                NotifyPropertyChange("MinutesPerBox");
                NotifyPropertyChange("MinutesPerPallet");
                NotifyPropertyChange("NextCrateUp");
                NotifyPropertyChange("ChangeOver");
            }
        }


        #region Properties

        public string SummaryTitle
        {
            get 
            { 
                return string.Format("{0}:  #{1}   {2} ft   {3}", Line, Part.PartName, Part.CutLength, Job.Material); 
            }
        }


        CalculatedMetrics metrics;
        public CalculatedMetrics CalculatedValues 
        { 
            get { return metrics; }
            set
            {
                metrics = value;
                NotifyPropertyChange("Line");
                NotifyPropertyChange("TotalBoxes");
                NotifyPropertyChange("Material");
                NotifyPropertyChange("Parts");
                NotifyPropertyChange("MinutesPerBox");
                NotifyPropertyChange("MinutesPerPallet");
                NotifyPropertyChange("PinPoint");
                NotifyPropertyChange("Part");
                NotifyPropertyChange("BoxesPerCrate");

                NotifyPropertyChange("CurrentBox");
                NotifyPropertyChange("ShiftBoxNeeds");
                NotifyPropertyChange("ShiftPalletNeeds");
                NotifyPropertyChange("ShiftCrateNeeds");
                NotifyPropertyChange("MinutesPerBox");
                NotifyPropertyChange("MinutesPerPallet");
                NotifyPropertyChange("NextCrateUp");
                NotifyPropertyChange("ChangeOver");
            }
        }


        public float CurrentBox        
        {            
            get { return CalculatedValues.CurrentBox; }      
            set
            {
                CalculatedValues.CurrentBox = value;
                NotifyPropertyChange();
            }
        }

        public float ShiftBoxNeeds     
        {            
            get { return CalculatedValues.ShiftBoxNeeds; }     
            set
            {
                CalculatedValues.ShiftBoxNeeds = value;
                NotifyPropertyChange();
            }
        }

        public float ShiftPalletNeeds  
        {           
            get { return CalculatedValues.ShiftPalletNeeds; }  
            set
            {
                CalculatedValues.ShiftPalletNeeds = value;
                NotifyPropertyChange();
            }
        }

        public float ShiftCrateNeeds   
        {            
            get { return CalculatedValues.ShiftCrateNeeds; }   
            set
            {
                CalculatedValues.ShiftCrateNeeds = value;
                NotifyPropertyChange();
            }
        }

        public float MinutesPerBox     
        {            
            get { return CalculatedValues.MinutesPerBox; }     
            set
            {
                CalculatedValues.MinutesPerBox = value;
                NotifyPropertyChange();
            }
        }

        public float MinutesPerPallet  
        {            
            get { return CalculatedValues.MinutesPerPallet; }  
            set
            {
                CalculatedValues.MinutesPerPallet = value;
                NotifyPropertyChange();
            }
        }

        public DateTime ChangeOver     
        {            
            get { return CalculatedValues.ChangeOver; }        
            set
            {
                CalculatedValues.ChangeOver = value;
                NotifyPropertyChange();
            }
        }

        public DateTime NextCrateUp    
        {            
            get { return CalculatedValues.NextCrateUp; }       
            set
            {
                CalculatedValues.NextCrateUp = value;
                NotifyPropertyChange();
            }
        }


        public string Line
        {
            get { return job.Line; }
            set
            {
                job.Line = value;
                NotifyPropertyChange();
            }
        }


        public int BoxesPerCrate 
        { 
            get { return Job.BoxesPerCrate; } 
            set
            {
                Job.BoxesPerCrate = value;
                this.metrics = new CalculatedMetrics(this.job);
                NotifyPropertyChange();
            }
        }




        public int TotalBoxes
        {
            get { return job.TotalBoxes; }
            set
            {
                job.TotalBoxes = value;
                this.metrics = new CalculatedMetrics(this.job);
                NotifyPropertyChange();
            }
        }


        public string Material
        {
            get { return job.Material; }
            set
            {
                job.Material = value;
                NotifyPropertyChange();
            }
        }




        public Pin PinPoint
        {
            get { return job.PinPoint; }
            set
            {
                job.PinPoint = value;
                this.metrics = new CalculatedMetrics(this.job);
                NotifyPropertyChange();
            }
        }


        public Product Part
        {
            get { return job.Part; }
            set
            {
                job.Part = value;
                this.metrics = new CalculatedMetrics(this.job);
                NotifyPropertyChange();
            }
        }

        #endregion
    }
}
