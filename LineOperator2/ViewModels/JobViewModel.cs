using LineOperator2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LineOperator2.ViewModels
{
    public class JobViewModel : BaseViewModel
    {
        private Job job;




        public JobViewModel(Job source)
        {
            job = source;
            CalculatedValues = new CalculatedMetrics(job);
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
                NotifyPropertyChange("NextChangeOver");
            }
        }


        #region Properties

        CalculatedMetrics metrics;
        public CalculatedMetrics CalculatedValues 
        { 
            get { return metrics; }
            set
            {
                metrics = value;
                NotifyPropertyChange("");
                //NotifyPropertyChange("CurrentBox");
                //NotifyPropertyChange("ShiftBoxNeeds");
                //NotifyPropertyChange("ShiftPalletNeeds");
                //NotifyPropertyChange("ShiftCrateNeeds");
                //NotifyPropertyChange("MinutesPerBox");
                //NotifyPropertyChange("MinutesPerPallet");
                //NotifyPropertyChange("NextCrateUp");
                //NotifyPropertyChange("NextChangeOver");
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


        public ObservableCollection<Product> Parts
        {
            get { return job.Parts; }
            set
            {
                job.Parts = value;
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
