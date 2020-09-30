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
    public partial class PickPartForLinePage : ContentPage
    {
        JobViewModel jobViewModel;

        public PickPartForLinePage(JobViewModel inJob)
        {
            InitializeComponent();
            jobViewModel = inJob;

            this.partsList.ItemsSource = Database.GetParts();
            this.partsList.SelectedItem = jobViewModel.Part;
        }


        async private void OnAddPartClicked(object sender, EventArgs e)
        {
            jobViewModel.Part = new Product();
            await Navigation.PushAsync(new AddOrModifyPartPage(jobViewModel.Part));
        }


        async private void OnEditPartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddOrModifyPartPage(jobViewModel.Part));
        }


        async private void HandlePartSelection(object sender, SelectedItemChangedEventArgs e)
        {
            jobViewModel.Part = (Product)e.SelectedItem;
            await Navigation.PopAsync();
        }
    }
}