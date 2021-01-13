using LineOperator2.Services;
using LineOperator2.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

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

        async private void OnReportStatus(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TotalPackagingPage());
        }

        
        private void OnGetDB(object sender, System.EventArgs e)
        {

            //After copying this I was able to use Android Device explorer to copy it from this path
            ///storage/emulated/legacy/Android/data/com.companyname/files/etcdb.sl3
            //copy database to external storage.
            /*     string dbpath = Database.GetDBFileName();
                 string externalpath = Path.Combine(DependencyService.Get<IExternalStoragePath>().GetExternalStoragePath(), "etcdb.sl3");
                 Console.WriteLine($"Copying file from {dbpath} to {externalpath}");
                 File.Copy(dbpath, externalpath);
                 */

            List<string> filePaths = DependencyService.Get<IExternalStoragePath>().GetExternalStoragePath();
            foreach(var f in filePaths)
            {
                Console.WriteLine(f);
            }
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