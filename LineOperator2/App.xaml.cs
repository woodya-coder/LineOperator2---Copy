using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LineOperator2.Services;
using LineOperator2.Views;
using LineOperator2.ViewModels;
using LineOperator2.Models;

namespace LineOperator2
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //Job job = new Job
            //{
            //    Line = "Line 0",
            //    Material = "5104 1168",
            //    Part = new Product
            //    {
            //        Title = "Part 0 Title",
            //        PartName = "Part 0",
            //        CutLength = 12f,
            //        BoxesPerCrate = 35,
            //        PartsPerBox = 100
            //    },
            //    PinPoint = new Pin
            //    {
            //        BoxNumber = 1,
            //        LineSpeed = 40,
            //        PartialCount = 1,
            //        PinTime = DateTime.Now
            //    }
            //};

            //job.CalculateValues();

            //job.Write();

            MainPage = new NavigationPage(new MainPage());
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Database.SaveJobs();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
