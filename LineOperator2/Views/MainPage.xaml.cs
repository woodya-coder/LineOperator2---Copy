using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LineOperator2.Models;



namespace LineOperator2.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<string, NavigationPage> MenuPages = new Dictionary<string, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

        //    MenuPages.Add("Line 0", (NavigationPage)Detail);
        }


        public async Task NavigateFromMenu(string lineID)
        {
            Database.SendDBToDev();
        }

    }
}