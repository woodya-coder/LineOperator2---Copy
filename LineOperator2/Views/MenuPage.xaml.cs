using LineOperator2.Models;
using LineOperator2.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using LineOperator2.Services;
using System.Threading.Tasks;

namespace LineOperator2.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<JobViewModel> menuItems = new List<JobViewModel>();

        public MenuPage()
        {
            InitializeComponent();

            menuItems = Database.GetListOfJobViews();

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = ((JobViewModel)e.SelectedItem).Line;
          //      await RootPage.NavigateFromMenu(id);
            };
        }

        async private void OnPackagingClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TotalPackagingPage());

            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(100);
        }
    }
}