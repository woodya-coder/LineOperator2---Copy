﻿using LineOperator2.Services;
using LineOperator2.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LineOperator2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LineDetailPage : ContentPage
    {
        JobViewModel jobViewModel;

        public LineDetailPage(string lineID)
        {

            InitializeComponent();
            jobViewModel = new JobViewModel(Database.GetJob(lineID));
            try
            {
                this.BindingContext = jobViewModel;
            }
            catch(Exception e)
            {

            }
        }


        async private void OnPinClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PinLinePage(jobViewModel));
        }


        async private void OnUpdateJobClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateJobInfoPage(jobViewModel));
         //   jobViewModel.Write();

        }


        async private void OnNewJobClicked(object sender, EventArgs e)
        {
            var newjob = new Job { Line = jobViewModel.Line };
            jobViewModel.Job = newjob;
            await Navigation.PushAsync(new UpdateJobInfoPage(jobViewModel),true);
        }
    }
}