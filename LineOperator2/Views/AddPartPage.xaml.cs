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
    public partial class AddPartPage : ContentPage
    {
        readonly JobViewModel jobViewModel;

        public AddPartPage(JobViewModel inJob)
        {
            InitializeComponent();
            this.jobViewModel = inJob;
            if(inJob.Part != null)
            {
                this.partName.Text = inJob.Part.PartName;
                this.cutLength.Text = inJob.Part.CutLength.ToString();
                this.partsPerBox.Text = inJob.Part.PartsPerBox.ToString();
                this.boxLength.Text = inJob.Part.BoxLength;
                this.palletSize.Text = inJob.Part.PalletSize;
                this.crateSize.Text = inJob.Part.CrateSize;
                this.multiplier.Text = inJob.Part.Multiplier.ToString();
            }
        }


        async private void OnCancelled(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        async private void OnConfirmed(object sender, EventArgs e)
        {
            var newPart = new Product
            {
                PartName = this.partName.Text,
                CrateSize = this.crateSize.Text,
                BoxLength = this.boxLength.Text,
                PalletSize = this.palletSize.Text
            };

            if(float.TryParse(this.cutLength.Text, out float tempFloat))
                newPart.CutLength = tempFloat;

            if(int.TryParse(this.partsPerBox.Text, out int tempInt))
                newPart.PartsPerBox = tempInt;

            if (int.TryParse(this.multiplier.Text, out tempInt))
                newPart.Multiplier = tempInt;

            // submit it to the database or whatever.
            Database.AddOrUpdate(newPart);

            jobViewModel.Part = newPart;

            await Navigation.PopAsync();
        }
    }
}