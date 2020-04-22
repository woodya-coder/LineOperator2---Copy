using LineOperator2.Services;
using LineOperator2.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace LineOperator2.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Dictionary<string, LineDetailPage> LineViewPages = new Dictionary<string, LineDetailPage>();
        public MainPage()
        {
            InitializeComponent();

            var lineViewModels = Database.GetListOfJobViews();
            this.lines.ItemsSource = lineViewModels;

            foreach(var jobview in lineViewModels)
            {
                LineViewPages.Add(jobview.Line, new LineDetailPage(jobview));
            }

        }

        async private void OnLineTapped(object sender, ItemTappedEventArgs e)
        {
            ListView list = sender as ListView;
            JobViewModel model = list.SelectedItem as JobViewModel;
            //await Navigation.PushAsync(new TitledNavigationPage(LineViewPages[model.Line], string.Format("{0}:  #{1}   {2} ft   {3}", model.Line, model.Part.PartName,model.Part.CutLength,model.Job.Material)));
            await Navigation.PushAsync(LineViewPages[model.Line]);
        }

        private void OnReportStatus(object sender, System.EventArgs e)
        {

        }


        //public async Task NavigateFromMenu(string lineID)
        //{
        //    if (!MenuPages.ContainsKey(lineID))
        //    {
        //        MenuPages.Add(lineID, new NavigationPage(new LineDetailPage(lineID)));
        //    }

        //    var newPage = MenuPages[lineID];

        //    if (newPage != null && Detail != newPage)
        //    {
        //        Detail = newPage;

        //        if (Device.RuntimePlatform == Device.Android)
        //            await Task.Delay(100);

        //        IsPresented = false;
        //    }
        //}
    }
}