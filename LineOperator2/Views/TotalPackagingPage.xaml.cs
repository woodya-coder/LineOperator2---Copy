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

    public class Packaging
    {
        public string Line { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
        public int Multiple { get; set; }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TotalPackagingPage : ContentPage
    {
        List<Packaging> packages = new List<Packaging>();

        public TotalPackagingPage()
        {
            InitializeComponent();

            //For each job in the database, calculate the metrics
            //Set the binding context to this list of calculated metrics.
            var jobList = Database.GetListOfJobViews();
            foreach(var job in jobList)
            {

                var package = new Packaging();
                var metric = new CalculatedMetrics(job.Job);
                if (job.Part != null && !job.IsTooOld)
                {
                    if (!string.IsNullOrWhiteSpace(job.Part.BoxLength) && metric.ShiftBoxNeeds > 0)
                    {
                        packages.Add(new Packaging()
                        {
                            Line = job.Line,
                            Value = metric.ShiftBoxNeeds,
                            Description = job.Part.BoxLength + " Boxes",
                            Multiple = job.Part.Multiplier
                        });
                    }

                    if (!string.IsNullOrWhiteSpace(job.Part.CrateSize) && metric.ShiftCrateNeeds > 0)
                    {
                        packages.Add(new Packaging()
                        {
                            Line = job.Line,
                            Value = metric.ShiftCrateNeeds,
                            Description = job.Part.CrateSize + " Crates",
                            Multiple = job.Part.Multiplier
                        });
                    }

                    if (!string.IsNullOrWhiteSpace(job.Part.PalletSize) && metric.ShiftPalletNeeds > 0)
                    {
                        packages.Add(new Packaging()

                        {
                            Line = job.Line,
                            Value = metric.ShiftPalletNeeds,
                            Description = job.Part.PalletSize + " Pallets",
                            Multiple = job.Part.Multiplier
                        });
                    }
                }
            }

            var q = from d in packages orderby d.Description ascending select d;
            this.jobList.ItemsSource = q;
        }

        async private void OnReturnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}