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
        readonly string CreatePartString = "Create New Part";

        public UpdateJobInfoPage(JobViewModel parentview, bool isNew = false)
        {
            InitializeComponent();
            this.isNew = isNew;
            viewModel =  parentview;
            this.BindingContext = viewModel;

            var partList = Database.GetPartNames();
            partList.Insert(0, CreatePartString);
            this.partPicker.ItemsSource = partList;

            this.totalBoxes.Text = viewModel?.TotalBoxes.ToString();
            this.boxesPerCrate.Text = viewModel?.BoxesPerCrate.ToString();
            this.material.Text = viewModel?.Material;
        }


        //async private void OnPickPartClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new PickPartForLinePage(viewModel));
        //}


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
                    LastCompleteBoxNum = 1,
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


        async private void OnPartSelectPicked(object sender, EventArgs e)
        {
            if (partPicker.SelectedIndex == -1)
                return;

            Picker picker = sender as Picker;

            string partName = (string)picker.SelectedItem;
            if(partName == this.CreatePartString)
            {
                viewModel.Part = new Product();
                await Navigation.PushAsync(new AddOrModifyPartPage(viewModel.Part));
            }
            else
            {
                viewModel.Job.Part = Database.GetPart(partName);
            }
            partPicker.SelectedIndex = -1;
        }


        async private void OnPartModifyClicked(object sender, EventArgs e)
        {
            //if there is a selected product then that's what we are updating.
            if(!string.IsNullOrEmpty(this.viewModel.Part.PartName))
            {
                await Navigation.PushAsync(new AddOrModifyPartPage(viewModel.Part));
            }

        }
    }
}