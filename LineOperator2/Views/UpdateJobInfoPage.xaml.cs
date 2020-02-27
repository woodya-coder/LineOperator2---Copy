using LineOperator2.Models;
using LineOperator2.Services;
using LineOperator2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LineOperator2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateJobInfoPage : ContentPage
    {
        JobViewModel viewModel;
        bool isNew;

        public UpdateJobInfoPage(JobViewModel parentview, bool isNew = false)
        {
            InitializeComponent();
            this.isNew = isNew;
            viewModel =  parentview;
            this.BindingContext = viewModel;

            this.totalBoxes.Text = viewModel?.TotalBoxes.ToString();
            this.boxesPerCrate.Text = viewModel?.BoxesPerCrate.ToString();
            this.material.Text = viewModel?.Material;
        }


        async private void OnPickPartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PickPartForLinePage(viewModel));
        }


        async private void OnUpdateClicked(object sender, EventArgs e)
        {
            if (int.TryParse(this.totalBoxes.Text, out int result))
            {
                viewModel.TotalBoxes = result;
            }

            viewModel.Material = this.material.Text;

            if (int.TryParse(this.boxesPerCrate.Text, out result))
            {
                viewModel.BoxesPerCrate = result;
            }

            if(this.isNew)
            {
                var p = new Pin
                {
                    BoxNumber = 1,
                    PartialCount = 0,
                    SampleWeight = 0f,
                    PinTime = DateTime.Now,
                    LineSpeed = 0,              //#TODO: Look up historical runs and put the average here. same for weight above.
                    JobID = viewModel.Job.ID

                };
                viewModel.PinPoint = p;
            }
            
            viewModel.CalculatedValues.UpdateCalculationsFrom(viewModel.Job);
            viewModel.Job.Write();
            await Navigation.PopAsync();
        }
    }
}