using LineOperator2.Models;
using LineOperator2.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LineOperator2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrModifyPartPage : ContentPage
    {
        readonly Product part;

        public bool IsEditing { get; set; }

        public AddOrModifyPartPage(Product inPart)
        {
            InitializeComponent();
            this.part = inPart ?? throw new NullReferenceException("A reference to a valid Product object must be passed to the constructor for AddOrModifyPartPage");

            if (!string.IsNullOrEmpty(inPart.PartName))
            {
                this.partName.Text = this.part.PartName;
                this.cutLength.Text = this.part.CutLength.ToString();
                this.partsPerBox.Text = this.part.PartsPerBox.ToString();
                this.boxLength.Text = this.part.BoxLength;
                this.palletSize.Text = this.part.PalletSize;
                this.crateSize.Text = this.part.CrateSize;
                this.multiplier.Text = this.part.Multiplier.ToString();
                this.IsEditing = true;
            }
        }


        async private void OnCancelled(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        async private void OnConfirmed(object sender, EventArgs e)
        {
            this.part.PartName = this.partName.Text;
            this.part.CrateSize = this.crateSize.Text;
            this.part.BoxLength = this.boxLength.Text;
            this.part.PalletSize = this.palletSize.Text;

            if(float.TryParse(this.cutLength.Text, out float tempFloat))
                this.part.CutLength = tempFloat;

            if(int.TryParse(this.partsPerBox.Text, out int tempInt))
                this.part.PartsPerBox = tempInt;

            if (int.TryParse(this.multiplier.Text, out tempInt))
                this.part.Multiplier = tempInt;

            // submit it to the database or whatever.
            Database.AddOrUpdate(this.part);

            await Navigation.PopAsync();
        }
    }
}