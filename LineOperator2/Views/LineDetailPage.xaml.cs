using LineOperator2.Services;
using LineOperator2.ViewModels;
using System;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LineOperator2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LineDetailPage : ContentPage
    {
        JobViewModel jobViewModel;

        public LineDetailPage(JobViewModel model)
        {

            InitializeComponent();

            
            jobViewModel = model;
            try
            {
                this.BindingContext = model;
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

        async  private void OnEditPartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddOrModifyPartPage(jobViewModel.Part));
            jobViewModel.RefreshTitle();
        }


        async private void OnEndOfShift(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EndOfShiftPage(jobViewModel));

        }
    }
}