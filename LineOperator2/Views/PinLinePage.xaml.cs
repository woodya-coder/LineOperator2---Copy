using LineOperator2.Models;
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
    public partial class PinLinePage : ContentPage
    {
        JobViewModel viewmodel;

        public PinLinePage(JobViewModel parentview)
        {
            InitializeComponent();
            this.viewmodel = parentview;
            this.Time.Text = DateTime.Now.ToShortTimeString();
            this.lineSpeed.Text = string.Format("{0:f1}", this.viewmodel.PinPoint?.LineSpeed ?? 0);
            this.sampleWeight.Text = string.Format("{0:f1}", this.viewmodel?.PinPoint.SampleWeight ?? 0);

            //Using the last pin point, calculate what box we should be on at this point.
            // number of minutes elapsed = now - last pin time
            var elapsedMinutes = DateTime.Now - viewmodel.PinPoint.PinTime;


            // number of minutes elapsed / minutes per box
            double exactCurrentBox = 0f;
            if (viewmodel.MinutesPerBox != 0)
            {
                exactCurrentBox = elapsedMinutes.TotalMinutes / viewmodel.MinutesPerBox;
            }

            // The result rounded up is the current box.
            var currentBox = viewmodel.PinPoint.BoxNumber +  (int)(exactCurrentBox + 1);
            this.boxNumber.Text = currentBox.ToString();

            // The fractional portion times the number of parts per box is the partial count
            int partsPerBox = 0;
            if(viewmodel.Part != null)
            {
                partsPerBox = viewmodel.Part.PartsPerBox;
            }

            var partialCount = (int)((exactCurrentBox - (int)exactCurrentBox) * partsPerBox);
            this.partialCount.Text = partialCount.ToString(); 
        }


        async private void OnPinClicked(object sender, EventArgs e)
        {
            Pin p = new Pin();

            if (int.TryParse(this.boxNumber.Text, out int iResult))
                p.BoxNumber = iResult;

            if (int.TryParse(this.partialCount.Text, out iResult))
                p.PartialCount = iResult;

            if (float.TryParse(this.lineSpeed.Text, out float fResult))
                p.LineSpeed = fResult;

            if (float.TryParse(this.sampleWeight.Text, out fResult))
                p.SampleWeight = fResult;

            if (DateTime.TryParse(this.Time.Text, out DateTime dResult))
                p.PinTime = dResult;

            this.viewmodel.PinPoint = p;
            this.viewmodel.CalculatedValues = new CalculatedMetrics(this.viewmodel.Job);
            this.viewmodel.Job.Write();

            await Navigation.PopAsync();
        }
    }
}