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
    public partial class EndOfShiftPage : ContentPage
    {
        private JobViewModel jobViewModel;

        public EndOfShiftPage(JobViewModel model)
        {
            InitializeComponent();

            jobViewModel = model;
            try
            {
                this.BindingContext = model;
            }
            catch (Exception e)
            {

            }
        }
    }
}